using PhyJSON.JSONParsers;
using PhyJSON.NodeParser;
using PhyJSON.Nodes;
using PHyJSONWinforms.JSONManagers;
using PHyJSONWinforms.JSONManagers.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PHyJSONWinforms
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// JSONManager
        /// </summary>
        private JSONManager JSONManager = new JSONManager();

        /// <summary>
        /// Flattened Structure
        /// </summary>
        private List<NewNode> FlattenedNodes = new List<NewNode>();

        /// <summary>
        /// Reference  to the nodes we can use this to convert back to JSON
        /// </summary>
        private KeyValueNode KeyValueNode { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public Form1()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Random button
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            // cast back to JSON
            string json = JSONManager.ParseToJSON(KeyValueNode);

        }

        /// <summary>
        /// Load data
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            if (KeyValueNode == null)
            {
                SetupJson();
            }
        }
        
        public void SetupJson()
        {
            // keyValueNode of our object
            KeyValueNode = JSONManager.GetKeyValueNode();

            // transform the keyValueNode into a listview
            LoadKeyValueNode(KeyValueNode, null, 0);

            DataGridView.DataSource = FlattenedNodes;

            // how do we convert the flattened nodes back?
        }

        public void LoadArrayNode(ArrayNode node, NewNode currentNode, int counter)
        {
            var key = node.Index;

            if (currentNode == null)
            {
                currentNode = new NewNode();
                FlattenedNodes.Add(currentNode);
            }
            currentNode.AddKeyNode(node, "[" + key + "]");

            foreach (var nodeValue in node.Values)
            {
                if (currentNode.HasValueNode)
                {
                    var nodes = currentNode.GetNodes();
                    var currentNode2 = new NewNode();
                    foreach (var n in nodes.Take(counter))
                    {
                        currentNode2.AddKeyNode(n.Key, n.Value);
                    }
                    currentNode2.AddKeyNode(node, "[" + key + "]");
                    FlattenedNodes.Add(currentNode2);
                    currentNode = currentNode2;
                }

                if (nodeValue is ArrayNode)
                {
                    var arrayNode = nodeValue as ArrayNode;
                    LoadArrayNode(arrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyArrayNode)
                {
                    var keyArrayNode = nodeValue as KeyArrayNode;
                    LoadKeyArrayNode(keyArrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyValueNode)
                {
                    var keyValueNode = nodeValue as KeyValueNode;
                    LoadKeyValueNode(keyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ListKeyValueNode)
                {
                    var listKeyValueNode = nodeValue as ListKeyValueNode;
                    LoadListKeyValueNode(listKeyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ValueNode)
                {
                    var valueNode = nodeValue as ValueNode;
                    LoadValueNode(valueNode, currentNode);
                }
                else if (nodeValue == null)
                {
                    var valueNode = new ValueNode();
                    valueNode.Value = "";
                    currentNode.SetValueNode(valueNode);
                }
            }
        }

        public void LoadKeyArrayNode(KeyArrayNode node, NewNode currentNode, int counter)
        {
            var key = node.Key;

            if (currentNode == null)
            {
                currentNode = new NewNode();
                FlattenedNodes.Add(currentNode);
            }
            currentNode.AddKeyNode(node, key);

            foreach (var nodeValue in node.Values)
            {
                if (currentNode.HasValueNode)
                {
                    var nodes = currentNode.GetNodes();
                    var currentNode2 = new NewNode();
                    foreach (var n in nodes.Take(counter))
                    {
                        currentNode2.AddKeyNode(n.Key, n.Value);
                    }
                    currentNode2.AddKeyNode(node, key);
                    FlattenedNodes.Add(currentNode2);
                    currentNode = currentNode2;
                }

                if (nodeValue is ArrayNode)
                {
                    var arrayNode = nodeValue as ArrayNode;
                    LoadArrayNode(arrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyArrayNode)
                {
                    var keyArrayNode = nodeValue as KeyArrayNode;
                    LoadKeyArrayNode(keyArrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyValueNode)
                {
                    var keyValueNode = nodeValue as KeyValueNode;
                    LoadKeyValueNode(keyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ListKeyValueNode)
                {
                    var listKeyValueNode = nodeValue as ListKeyValueNode;
                    LoadListKeyValueNode(listKeyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ValueNode)
                {
                    var valueNode = nodeValue as ValueNode;
                    LoadValueNode(valueNode, currentNode);
                }
                else if (nodeValue == null)
                {
                    var valueNode = new ValueNode();
                    valueNode.Value = "";
                    currentNode.SetValueNode(valueNode);
                }
            }
        }

        public void LoadKeyValueNode(KeyValueNode node, NewNode currentNode, int counter)
        {
            string key = node.Key;

            if (currentNode == null)
            {
                currentNode = new NewNode();
                FlattenedNodes.Add(currentNode);
            }
            currentNode.AddKeyNode(node, key);

            if (node.Value is ArrayNode)
            {
                var arrayNode = node.Value as ArrayNode;
                LoadArrayNode(arrayNode, currentNode, counter + 1);
            }
            else if (node.Value is KeyArrayNode)
            {
                var keyArrayNode = node.Value as KeyArrayNode;
                LoadKeyArrayNode(keyArrayNode, currentNode, counter + 1);
            }
            else if (node.Value is KeyValueNode)
            {
                var keyValueNode = node.Value as KeyValueNode;
                LoadKeyValueNode(keyValueNode, currentNode, counter + 1);
            }
            else if (node.Value is ListKeyValueNode)
            {
                var listKeyValueNode = node.Value as ListKeyValueNode;
                LoadListKeyValueNode(listKeyValueNode, currentNode, counter + 1);
            }
            else if (node.Value is ValueNode)
            {
                var valueNode = node.Value as ValueNode;
                LoadValueNode(valueNode, currentNode);
            }
            else if (node.Value == null)
            {
                var valueNode = new ValueNode();
                valueNode.Value = "";
                currentNode.SetValueNode(valueNode);
            }
        }

        public void LoadListKeyValueNode(ListKeyValueNode node, NewNode currentNode, int counter)
        {
            var key = node.Index;

            if (currentNode == null)
            {
                currentNode = new NewNode();
                FlattenedNodes.Add(currentNode);
            }
            currentNode.AddKeyNode(node, "[" + key + "]");

            foreach (var nodeValue in node.Values)
            {
                if (currentNode.HasValueNode)
                {
                    var nodes = currentNode.GetNodes();
                    var currentNode2 = new NewNode();
                    foreach (var n in nodes.Take(counter))
                    {
                        currentNode2.AddKeyNode(n.Key, n.Value);
                    }
                    currentNode2.AddKeyNode(node, "[" + key + "]");
                    FlattenedNodes.Add(currentNode2);
                    currentNode = currentNode2;
                }

                if (nodeValue is ArrayNode)
                {
                    var arrayNode = nodeValue as ArrayNode;
                    LoadArrayNode(arrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyArrayNode)
                {
                    var keyArrayNode = nodeValue as KeyArrayNode;
                    LoadKeyArrayNode(keyArrayNode, currentNode, counter + 1);
                }
                else if (nodeValue is KeyValueNode)
                {
                    var keyValueNode = nodeValue as KeyValueNode;
                    LoadKeyValueNode(keyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ListKeyValueNode)
                {
                    var listKeyValueNode = nodeValue as ListKeyValueNode;
                    LoadListKeyValueNode(listKeyValueNode, currentNode, counter + 1);
                }
                else if (nodeValue is ValueNode)
                {
                    var valueNode = nodeValue as ValueNode;
                    LoadValueNode(valueNode, currentNode);
                }
                else if (nodeValue == null)
                {
                    var valueNode = new ValueNode();
                    valueNode.Value = "";
                    currentNode.SetValueNode(valueNode);
                }
            }
        }

        public void LoadValueNode(ValueNode node, NewNode currentNode)
        {
            currentNode.SetValueNode(node);
        }
    }
}
