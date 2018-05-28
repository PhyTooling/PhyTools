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

namespace PhyTools.Applications.TestForm
{
    /// <summary>
    /// Interaction logic for TestFormUserControl.xaml
    /// </summary>
    public partial class TestFormUserControl : UserControl
    {
        public event PrintToConsole PrintToConsoleEvent;

        public TestFormUserControl()
        {
            InitializeComponent();
        }

        private void Button_Click_SendLog(object sender, RoutedEventArgs e)
        {
            if (PrintToConsoleEvent != null)
            {
                PrintToConsoleEvent.Invoke(null, new PrintToConsoleEvent()
                {
                    Text = TextBoxPrintToConsole.Text,
                });
            }
        }
    }
}
