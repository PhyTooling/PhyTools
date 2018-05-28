using PhyTools.Applications.ServiceManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PhyTools.Applications.ServiceManagers
{
    /// <summary>
    /// Interaction logic for ServiceWindow.xaml
    /// </summary>
    public partial class ServiceWindow : Window
    {
        private WindowsApplicationServiceManager WindowsApplicationServiceManager { get; set; }

        public ServiceWindow(WindowsApplicationService windowsApplicationService)
        {
            InitializeComponent();
            TextBoxServiceName.Text = windowsApplicationService.DisplayName;
            TextBoxDescription.Text = windowsApplicationService.Description;

            int index = -1;
            ServiceStartMode serviceStartMode;
            if (!Enum.TryParse(windowsApplicationService.StartType, out serviceStartMode))
            {
                MessageBox.Show("Unable to parse the Startup Type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                for (int x = 0; x < ComboBoxStartupType.Items.Count; x++)
                {
                    var item = ComboBoxStartupType.Items[x] as ComboBoxItem;
                    var content = item.Content as string;
                    if (content == serviceStartMode.ToString())
                    {
                        index = x;
                        break;
                    }
                }
            }

            index = -1;
            if (!string.IsNullOrEmpty(windowsApplicationService.ServiceAccountName))
            {
                ServiceAccount serviceAccount = windowsApplicationService.ServiceAccount;
                for (int x = 0; x < ComboBoxServiceAccount.Items.Count; x++)
                {
                    var item = ComboBoxServiceAccount.Items[x] as ComboBoxItem;
                    var content = item.Content as string;
                    if (content == serviceAccount.ToString())
                    {
                        index = x;
                        break;
                    }
                }
            }
            ComboBoxServiceAccount.SelectedIndex = index;

            ButtonInstallUpdate.Content = "Update Service";
            ButtonInstallUpdate.Click += Button_Click_UpdateService;
        }

        public ServiceWindow(WindowsApplicationServiceManager windowsApplicationServiceManager)
        {
            WindowsApplicationServiceManager = windowsApplicationServiceManager;
            InitializeComponent();
            ButtonInstallUpdate.Content = "Install New Service";
            ButtonInstallUpdate.Click += Button_Click_InstallNewService;
        }

        private void Button_Click_UpdateService(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceName = TextBoxServiceName.Text;
                var serviceDisplayName = TextBoxServiceName.Text;
                var description = TextBoxDescription.Text;
                var serviceDependancies = new string[] { };

                if (string.IsNullOrEmpty(serviceName))
                {
                    MessageBox.Show("ServiceName is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("Description is empty", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ServiceStartMode serviceStartMode;
                if (!Enum.TryParse(ComboBoxStartupType.Text, out serviceStartMode))
                {
                    MessageBox.Show("Unable to parse the Startup Type", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ServiceAccount serviceAccount;
                if (!Enum.TryParse(ComboBoxServiceAccount.Text, out serviceAccount))
                {
                    MessageBox.Show("Unable to parse the Account", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                MessageBox.Show("Edit function not implemented.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);

                //MessageBox.Show("Service Updated.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Failed to create service. {0} {1}", exception.Message, exception.StackTrace), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void Button_Click_InstallNewService(object sender, RoutedEventArgs e)
        {
            try
            {
                var serviceName = TextBoxServiceName.Text;
                var serviceDisplayName = TextBoxServiceName.Text;
                var description = TextBoxDescription.Text;
                var serviceDependancies = new string[] { };

                if (string.IsNullOrEmpty(serviceName))
                {
                    MessageBox.Show("ServiceName is empty", "Failed Validation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (string.IsNullOrEmpty(description))
                {
                    MessageBox.Show("Description is empty", "Failed Validation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ServiceStartMode serviceStartMode;
                if (!Enum.TryParse(ComboBoxStartupType.Text, out serviceStartMode))
                {
                    MessageBox.Show("Unable to parse the Startup Type", "Failed Validation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                ServiceAccount serviceAccount;
                if (!Enum.TryParse(ComboBoxServiceAccount.Text, out serviceAccount))
                {
                    MessageBox.Show("Unable to parse the Account", "Failed Validation", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                WindowsApplicationServiceManager.InstallService(serviceName, serviceDisplayName, description, serviceDependancies, serviceStartMode, serviceAccount);

                MessageBox.Show("Service Created.", "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                this.Close();
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Failed to create service. {0} {1}", exception.Message, exception.StackTrace), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
