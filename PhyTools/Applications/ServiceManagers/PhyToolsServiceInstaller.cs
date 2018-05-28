using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Configuration.Install;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.ServiceManagers
{
    [RunInstaller(true)]
    public partial class PhyToolsServiceInstaller : Installer
    {
        public PhyToolsServiceInstaller(string serviceName, string displayName, string descrption, string[] servicesDependedOn, ServiceStartMode serviceStartMode, ServiceAccount serviceAccount)
        {
            ServiceInstaller serviceInstaller = new ServiceInstaller();

            serviceInstaller.ServiceName = serviceName;
            serviceInstaller.DisplayName = displayName;
            serviceInstaller.Description = descrption;
            serviceInstaller.ServicesDependedOn = servicesDependedOn;
            serviceInstaller.StartType = serviceStartMode;

            ServiceProcessInstaller installer = new ServiceProcessInstaller();
            installer.Account = serviceAccount;

            Installers.AddRange(new Installer[] { serviceInstaller, installer });
        }
    }
}
