using PhyTools.Delegates;
using PhyTools.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Web.Script.Serialization;
using Newtonsoft.Json;
using System.Collections;
using System.Diagnostics;
using PhyTools.Applications.JSON.TreeModels.TreeViewModel;
using PhyTools.Applications.JSON.TreeModels.ViewModels;
using PhyTools.Applications.JSON.Nodes;
using Newtonsoft.Json.Linq;
using PhyJSON.JSONParsers;
using PhyJSON.Nodes;

namespace PhyTools.Applications.JSONApplication
{
    /// <summary>
    /// Interaction logic for JSONUserControl
    /// </summary>
    public partial class JSONUserControl : UserControl
    {
        private JSONParser JSONParser = new JSONParser();
        private KeyValueNodeContainerViewModel viewModel { get; set; }

        public event PrintToConsole PrintToConsoleEvent;

        public JSONUserControl()
        {
            InitializeComponent();


            DoMagic();
        }

        public void DoMagic()
        {
            #region some random object to parse
            var dictionaryTest = new Dictionary<object, object>();
            dictionaryTest.Add(new Car(), "A-VALUE");
            dictionaryTest.Add("B-KEY", "B-VALUE");

            var test2 = new List<object>()
            {
                1,
                2,
            };

            var test = new List<object>()
            {
                dictionaryTest,
                1,
                test2,
                2
            };

            JSONTestObject jsonTestObject = new JSONTestObject()
            {
                Name = "test",
                Cars = new List<object>()
                {
                    test,
                    (new Car3() { CarNames = new List<object> () { new Car() { CarName = "3456" }, "1234" }, CarAge2 = "6666" }) as object,
                    (new Car2() { CarName2 = "5555", CarAge2 = "6666" }) as object,
                    (new Car() { CarName = "2" }) as object,
                    (1),
                },
                MyDictionary = dictionaryTest,
            };
            #endregion

            #region turn Object to JSON string
            string json = JSONParser.ParseToJSONString(jsonTestObject as object);
            #endregion

            Dictionary<string, object> jsonAsdictionary = JSONParser.ParseToDictionary(json);

            #region turn json into BasicNode
            // turn into a tree of basic nodes
            KeyValueNode rootNode = JSONParser.ParseToKeyValueNode(jsonAsdictionary);
            #endregion


            #region turn the BasicRoot back to JSON for validation
            // check if you can turn it back into a json
            var keyValueNodeAsDictionary = JSONParser.ParseToDictionary(rootNode);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string keyValueNodeAsJSON = js.Serialize(keyValueNodeAsDictionary);

            if (keyValueNodeAsJSON != json)
            {
                throw new Exception("failed to deserialise");
            }
            int x = 0;
            #endregion

            SetupTreeView(rootNode);
        }

        #region Setup our treeview
        private void SetupTreeView(KeyValueNode keyValueNode)
        {
            // setup categories list
            var keyValueNodes = new List<KeyValueNode>()
            {
                keyValueNode,
            };

            viewModel = new KeyValueNodeContainerViewModel(keyValueNodes);
            NewView.DataContext = viewModel;
        }
        #endregion

        private void Button_Click_ToJSON(object sender, RoutedEventArgs e)
        {
            try
            {
                var node = viewModel.RootTreeNodes.FirstOrDefault();
                if (node != null)
                {
                    var rootNode = node.GetRootNode();

                    var dictionary = JSONParser.ParseToDictionary(rootNode);

                    JavaScriptSerializer js = new JavaScriptSerializer();
                    string nodeJson = js.Serialize(dictionary);

                    string prettyJson = JToken.Parse(nodeJson).ToString(Formatting.Indented);

                    RichTextBoxConsoleLog.Document.Blocks.Clear();
                    RichTextBoxConsoleLog.Document.Blocks.Add(new Paragraph(new Run(prettyJson)));
                }
            }
            catch (Exception exception)
            {

            }
        }

        private string RemoveChariageReturns(string value)
        {
            if (String.IsNullOrEmpty(value))
            {
                return value;
            }
            string lineSeparator = ((char)0x2028).ToString();
            string paragraphSeparator = ((char)0x2029).ToString();

            return value.Replace("\r\n", string.Empty)
                        .Replace("\n", string.Empty)
                        .Replace("\r", string.Empty)
                        .Replace(lineSeparator, string.Empty)
                        .Replace(paragraphSeparator, string.Empty);
        }

        private void Button_Click_ToTree(object sender, RoutedEventArgs e)
        {
            try
            {
                string json2 = RemoveChariageReturns(string.Format(@"{0}", new TextRange(RichTextBoxConsoleLog.Document.ContentStart, RichTextBoxConsoleLog.Document.ContentEnd).Text));

                #region turn Object to JSON string
                string json = JToken.Parse(json2).ToString(Formatting.Indented);
                #endregion

                Dictionary<string, object> jsonAsdictionary = JSONParser.ParseToDictionary(json);

                #region turn json into BasicNode
                // turn into a tree of basic nodes
                KeyValueNode keyValueNode = JSONParser.ParseToKeyValueNode(jsonAsdictionary);
                #endregion


                #region turn the BasicRoot back to JSON for validation
                // check if you can turn it back into a json
                var keyValueNodeAsDictionary = JSONParser.ParseToDictionary(keyValueNode);

                JavaScriptSerializer js = new JavaScriptSerializer();
                string keyValueNodeAsJSON = js.Serialize(keyValueNodeAsDictionary);

                string prettyNodeJson = JToken.Parse(keyValueNodeAsJSON).ToString(Formatting.Indented);

                if (prettyNodeJson != json)
                {
                    throw new Exception("failed to deserialise");
                }
                #endregion

                SetupTreeView(keyValueNode);
            }
            catch (Exception exception)
            {

            }
        }

        private void NewView_ContextMenuOpening(object sender, ContextMenuEventArgs e)
        {
            var block = e.OriginalSource as TextBlock;

            if (block != null)
            {
                // if this is something we cannot have a menu for then return here to close the menu
                //return
            }
            e.Handled = true;
        }

        private void Button_Click_Format_XML(object sender, RoutedEventArgs e)
        {
            //string prettyJson = JToken.Parse(nodeJson).ToString(Formatting.Indented);
            //RichTextBoxConsoleLog.Document.Blocks.Clear();
            //RichTextBoxConsoleLog.Document.Blocks.Add(new Paragraph(new Run(prettyJson)));
        }

        protected void txtblk_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBox txt = (TextBox)((Grid)((TextBlock)sender).Parent).Children[1];
            txt.Visibility = Visibility.Visible;
            ((TextBlock)sender).Visibility = Visibility.Collapsed;
        }

        protected void txtbox_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBlock tb = (TextBlock)((Grid)((TextBox)sender).Parent).Children[0];
            tb.Text = ((TextBox)sender).Text;
            tb.Visibility = Visibility.Visible;
            ((TextBox)sender).Visibility = Visibility.Collapsed;
        }
    }
}