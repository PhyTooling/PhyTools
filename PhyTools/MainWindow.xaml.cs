using PhyTools.Applications;
using PhyTools.Applications.TestForm;
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

namespace PhyTools
{
   
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        /// <summary>
        /// Application Manager
        /// </summary>
        private ApplicationManager ApplicationManager = new ApplicationManager();

        /// <summary>
        /// Returns the SelectedUserControl
        /// </summary>
        private UserControl CurrentSelectedUserControl
        {
            get
            {
                if (ApplicationContainerGrid == null)
                {
                    return null;
                }
                if (ApplicationContainerGrid.Children == null || ApplicationContainerGrid.Children.Capacity == 0)
                {
                    return null;
                }
                return ApplicationContainerGrid.Children[0] as UserControl;
            }
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public MainWindow()
        {
            InitializeComponent();
        }

        /// <summary>
        /// On selecting a application. This function sets tthe ApplicationContainer up.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_Click_SelectApplication(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null)
            {
                return;
            }

            var content = button.Content as string;
            var userControl = ApplicationManager.GetUserControl(content, new PrintToConsole(OnPrintToConsoleEvent));
            if (userControl == null)
            {
                PrintToConsole(string.Format("No User control setup for this name {0}", content));
                return;
            }
            else if (userControl == CurrentSelectedUserControl)
            {
                PrintToConsole("You have already selected this user control.");
                return;
            }

            PrintToConsole(string.Format("Selecting User control {0}", content));
            ApplicationContainerGrid.Children.Clear();
            ApplicationContainerGrid.Children.Add(userControl);
        }

        /// <summary>
        /// If another user controller wants to print to the console use a event.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="printToConsoleEvent"></param>
        private void OnPrintToConsoleEvent(object sender, PrintToConsoleEvent printToConsoleEvent)
        {
            if (printToConsoleEvent == null)
            {
                return;
            }
            if (string.IsNullOrEmpty(printToConsoleEvent.Text))
            {
                return;
            }

            PrintToConsole(printToConsoleEvent.Text);
        }

        /// <summary>
        /// Print logs to the console.
        /// </summary>
        /// <param name="text"></param>
        private void PrintToConsole(string text)
        {
            var timeStamp = DateTime.UtcNow;
            var output = string.Format("[{0:dd/MM/yyyy H:mm:ss.ffff}]: {1}{2}", timeStamp, text, "\r");
            RichTextBoxConsoleLog.AppendText(output);
        }
    }
}
