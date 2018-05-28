using PhyTools.Applications.ServiceManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;
using PhyTools.Extentions;
using System.Management;
using System.Configuration.Install;
using System.ComponentModel;
using System.Collections;
using System.Reflection;

namespace PhyTools.Applications.ServiceManagers
{
    public class WindowsApplicationServiceManager
    {
        /// <summary>
        /// GetWindowsServices
        /// </summary>
        /// <returns></returns>
        public List<WindowsApplicationService> GetWindowsServices()
        {
            ServiceController[] scServices;
            scServices = ServiceController.GetServices();

            List<WindowsApplicationService> windowsApplicationServices = new List<WindowsApplicationService>();

            foreach (ServiceController scTemp in scServices)
            {
                var windowsApplicationService = scTemp.ToWindowsApplicationService();

                if (scTemp.Status == ServiceControllerStatus.Running)
                {
                }

                // ignore any services that has the path of windows
                if (windowsApplicationService.Path.ToLower().Contains("windows"))
                {
                    //continue;
                }

                windowsApplicationServices.Add(windowsApplicationService);
            }

            return windowsApplicationServices;
        }

        /// <summary>
        /// InstallService
        /// </summary>
        /// <param name="serviceName"></param>
        /// <param name="displayName"></param>
        /// <param name="descrption"></param>
        /// <param name="servicesDependedOn"></param>
        /// <param name="serviceStartMode"></param>
        /// <param name="serviceAccount"></param>
        public void InstallService(string serviceName, string displayName, string descrption, string[] servicesDependedOn, ServiceStartMode serviceStartMode, ServiceAccount serviceAccount)
        {
            TransactedInstaller installer = new TransactedInstaller();
            installer.Context = new InstallContext(null, null);
            installer.Context.Parameters["assemblyPath"] = @"D:\DataStoreWindowsServices\DataLoader\DataLoaderService.exe";
            installer.Installers.Add(new PhyToolsServiceInstaller(serviceName, displayName, descrption, servicesDependedOn, serviceStartMode, serviceAccount));

            installer.Install(new Hashtable());
        }

        /// <summary>
        /// UninstallService
        /// </summary>
        /// <param name="serviceName"></param>
        public void UninstallService(string serviceName)
        {
            System.ServiceProcess.ServiceInstaller ServiceInstallerObj = new System.ServiceProcess.ServiceInstaller();
            InstallContext Context = new InstallContext(null, null);
            ServiceInstallerObj.Context = Context;
            ServiceInstallerObj.ServiceName = serviceName;
            ServiceInstallerObj.Uninstall(null);
        }
    }
}
