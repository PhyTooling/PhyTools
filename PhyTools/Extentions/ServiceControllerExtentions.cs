using Microsoft.Win32;
using PhyTools.Applications.ServiceManagers.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Extentions
{
    public static class ServiceControllerExtentions
    {
        public static WindowsApplicationService ToWindowsApplicationService(this ServiceController serviceController)
        {
            string path = string.Empty;
            string description = string.Empty;
            string serviceAccountName = string.Empty;
            var dependentServices = serviceController.DependentServices;
            try
            {
                var wmiService = new ManagementObject("Win32_Service.Name='" + serviceController.ServiceName + "'");
                wmiService.Get();
                description = wmiService["Description"] as string;
                path = wmiService["PathName"] as string;
                RegistryKey localMachineKey = Registry.LocalMachine; //sets the regKey for Local Machine node in regedit.
                RegistryKey fileServiceKey = localMachineKey.OpenSubKey(@"SYSTEM\CurrentControlSet\Services\" + serviceController.ServiceName);
                serviceAccountName = fileServiceKey.GetValue("ObjectName") as string;
            }
            catch (Exception exception)
            {
                Console.WriteLine(string.Format("Unable to parse windows service. {0}", exception));
            }

            var windowsApplicationService = new WindowsApplicationService()
            {
                ServiceName = serviceController.ServiceName,
                DisplayName = serviceController.DisplayName,
                Description = description,
                Path = path,
                Status = serviceController.Status.ToString(),
                StartType = serviceController.StartType.ToString(),
                ServiceAccountName = serviceAccountName,
            };
            return windowsApplicationService;
        }
    }
}
