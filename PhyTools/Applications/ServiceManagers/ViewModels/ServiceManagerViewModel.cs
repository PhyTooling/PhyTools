using PhyTools.Applications.ServiceManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using PhyTools.RelayCommands;
using System.Windows.Data;
using System.ComponentModel;

namespace PhyTools.Applications.ServiceManagers.ViewModels
{
    public class ServiceManagerViewModel : ViewModel
    {
        #region Item Sources
        private ObservableCollection<WindowsApplicationService> WindowsApplicationServices { get; set; }
        public ICollectionView WindowsApplicationServicesView { get; set; }
        public string FilterText { get; set; }
        #endregion

        #region Binding Examples
        #endregion

        #region Constructors
        public ServiceManagerViewModel()
        {
            this.WindowsApplicationServices = new ObservableCollection<WindowsApplicationService>() { };

            // bind our ObservableCollection to a view
            WindowsApplicationServicesView = CollectionViewSource.GetDefaultView(WindowsApplicationServices);

            // Enable filtering
            WindowsApplicationServicesView.Filter = Filter;
        }

        #endregion

        #region Simulated Data-Retrieval Methods
        /// <summary>
        /// Simulate the population of data.  In reality this method would most likely be retrieving data from a database, web service, config file, etc.
        /// </summary>
        public void FakeGetDummyData()
        {
            //for (int x = 0; x < 10; x++)
            //{
            //    WindowsApplicationServices.Add(new WindowsApplicationService()
            //    {
            //        ServiceName = string.Format("Fake WindowsApplicationService {0}", x),
            //        Description = "This is fake data",
            //    });
            //}
        }
        #endregion

        /// <summary>
        /// Add a WindowsApplicationService
        /// </summary>
        /// <param name="windowsApplicationService"></param>
        public void AddWindowsApplicationService(WindowsApplicationService windowsApplicationService)
        {
            WindowsApplicationServices.Add(windowsApplicationService);
        }

        /// <summary>
        /// Force Refresh
        /// </summary>
        public void ForceRefresh()
        {
            WindowsApplicationServicesView.Refresh();
        }

        /// <summary>
        /// Filter by text
        /// </summary>
        /// <param name="item"></param>
        /// <returns></returns>
        private bool Filter(object item)
        {
            if (string.IsNullOrEmpty(FilterText))
            {
                // Display row
                return true;
            }

            var windowsApplicationService = item as WindowsApplicationService;
            if (windowsApplicationService == null)
            {
                // Display row
                return true;
            }

            if (windowsApplicationService.DisplayName.Contains(FilterText))
            {
                // Display row
                return true;
            }

            // Dont display row
            return false;
        }
    }
}
