using PhyTools.Applications.ServiceManagers.ViewModels;
using PhyTools.Delegates;
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

namespace PhyTools.Applications.ServiceManagers
{
    /// <summary>
    /// Interaction logic for ServiceManagerUserControl.xaml
    /// </summary>
    public partial class ServiceManagerUserControl : UserControl
    {
        public event PrintToConsole PrintToConsoleEvent;

        private ServiceManagerViewModel ServiceManagerViewModel { get; set; }
        private WindowsApplicationServiceManager WindowsApplicationServiceManager { get; set; }

        public ServiceManagerUserControl()
        {
            InitializeComponent();
            InitialiseData();
        }

        public void InitialiseData()
        {
            ServiceManagerViewModel = new ServiceManagerViewModel();
            WindowsApplicationServiceManager = new WindowsApplicationServiceManager();

            var services = WindowsApplicationServiceManager.GetWindowsServices();
            foreach(var service in services)
            {
                ServiceManagerViewModel.AddWindowsApplicationService(service);
            }

            DataGridWindowsApplicationServices.DataContext = ServiceManagerViewModel;
        }

        private void Button_Click_InstallNewService(object sender, RoutedEventArgs e)
        {
            ServiceWindow ServiceWindow = new ServiceWindow(WindowsApplicationServiceManager);
            ServiceWindow.ShowDialog();
        }

        private void Button_Click_RefreshServiceList(object sender, RoutedEventArgs e)
        {
            InitialiseData();
        }
        
        #region Filter data grid
        private void FilterTextBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            ServiceManagerViewModel.FilterText = FilterTextBox.Text;
            ServiceManagerViewModel.ForceRefresh();
        }

        private void FilterTextBox_MouseEnter(object sender, MouseEventArgs e)
        {
            FilterTextBox.Background = Brushes.White;
        }

        private void FilterTextBox_MouseLeave(object sender, MouseEventArgs e)
        {
            if (FilterTextBox.IsFocused == false)
            {
                BrushConverter bc = new BrushConverter();
                FilterTextBox.Background = (Brush)bc.ConvertFrom("#ecf5fe");
            }
        }

        private void FilterTextBox_GotFocus(object sender, RoutedEventArgs e)
        {
            FilterTextBox.Background = Brushes.White;
        }

        private void FilterTextBox_LostFocus(object sender, RoutedEventArgs e)
        {
            BrushConverter bc = new BrushConverter();
            FilterTextBox.Background = (Brush)bc.ConvertFrom("#ecf5fe");
        }
        #endregion
    }
}
