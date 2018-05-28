using PhyTools.Applications.ServiceManagers.ViewModels;
using PhyTools.RelayCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace PhyTools.Applications.ServiceManagers.Contracts
{
    public class WindowsApplicationService : ServiceManagerViewModel
    {
        /// <summary>
        /// Service Name
        /// </summary>
        public string ServiceName { get; set; }

        /// <summary>
        /// Display Name
        /// </summary>
        public string DisplayName { get; set; }

        /// <summary>
        /// Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Path
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Status
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// StartType
        /// </summary>
        public string StartType { get; set; }

        /// <summary>
        /// Logon As
        /// </summary>
        public string ServiceAccountName { get; set; }

        public ServiceAccount ServiceAccount
        {
            get
            {
                if (ServiceAccountName != null)
                {
                    switch (ServiceAccountName.ToLower())
                    {
                        case @"nt authority\localservice":
                        case @"NT AUTHORITY\LocalService":
                            return ServiceAccount.LocalService;
                        case @"nt authority\networkservice":
                        case @"NT AUTHORITY\NetworkService":
                            return ServiceAccount.NetworkService;
                        case @"localsystem":
                        case @"LocalSystem":
                            return ServiceAccount.LocalSystem;
                    }
                }

                return ServiceAccount.User;
            }
        }

        /// <summary>
        /// Start Command
        /// </summary>
        private ICommand _StartCommand;
        public ICommand StartCommand
        {
            get
            {
                if (_StartCommand == null)
                {
                    _StartCommand = new RelayCommand((o) =>
                    {
                        StartMethodCommand();
                    });
                }
                return _StartCommand;
            }
        }

        public void StartMethodCommand()
        {
            //TODO:  Start - windows service
        }

        /// <summary>
        /// Stop command
        /// </summary>
        private ICommand _StopCommand;
        public ICommand StopCommand
        {
            get
            {
                if (_StopCommand == null)
                {
                    _StopCommand = new RelayCommand((o) =>
                    {
                        StopMethodCommand();
                    });
                }
                return _StopCommand;
            }
        }

        public void StopMethodCommand()
        {
            //TODO:  Stop - windows service
        }

        /// <summary>
        /// Edit Command
        /// </summary>
        private ICommand _EditCommand;
        public ICommand EditCommand
        {
            get
            {
                if (_EditCommand == null)
                {
                    _EditCommand = new RelayCommand((o) =>
                    {
                        EditMethodCommand();
                    });
                }
                return _EditCommand;
            }
        }

        public void EditMethodCommand()
        {
            try
            {
                ServiceWindow serviceWindow = new ServiceWindow(this);
                serviceWindow.ShowDialog();
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Unable to edit service. {0} {1}", exception.Message, exception.StackTrace), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        /// <summary>
        /// Delete Command
        /// </summary>
        private ICommand _DeleteCommand;
        public ICommand DeleteCommand
        {
            get
            {
                if (_DeleteCommand == null)
                {
                    _DeleteCommand = new RelayCommand((o) =>
                    {
                        DeleteMethodCommand();
                    });
                }
                return _DeleteCommand;
            }
        }

        public void DeleteMethodCommand()
        {
            try
            {
                if (MessageBox.Show(string.Format("Are you sure you want to delete {0}", ServiceName), "Are you sure?", MessageBoxButton.YesNo) == MessageBoxResult.Yes)
                {
                    WindowsApplicationServiceManager windowsApplicationServiceManager = new WindowsApplicationServiceManager();
                    windowsApplicationServiceManager.UninstallService(ServiceName);
                    MessageBox.Show(string.Format("Service has been deleted {0}", ServiceName), "Success", MessageBoxButton.OK, MessageBoxImage.Information);
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(string.Format("Unable to uninstall service. {0} {1}", exception.Message, exception.StackTrace), "Error", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
