using PhyTools.Applications.JSONApplication;
using PhyTools.Applications.NotificationsApplication;
using PhyTools.Applications.PersonalManagerApplication;
using PhyTools.Applications.ServiceManagers;
using PhyTools.Applications.TestForm;
using PhyTools.Delegates;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace PhyTools.Applications
{
    /// <summary>
    /// Managers which the store of our application
    /// </summary>
    public class ApplicationManager
    {
        /// <summary>
        /// Store of the UserControls
        /// </summary>
        private Dictionary<string, UserControl> UserControls = new Dictionary<string, UserControl>();

        /// <summary>
        /// Returns the UserControl else returns null.
        /// </summary>
        /// <param name="text"></param>
        /// <param name="printToConsole"></param>
        /// <returns></returns>
        public UserControl GetUserControl(string text, PrintToConsole printToConsole, NotificationDelegate notificationDelegate)
        {
            UserControl userControl = null;

            if (UserControls.TryGetValue(text, out userControl))
            {
                return userControl;
            }

            switch (text)
            {
                case "TestForm":
                    TestFormUserControl testFormUserControl = new TestFormUserControl();
                    testFormUserControl.PrintToConsoleEvent += printToConsole;
                    testFormUserControl.NotificationCountEvent += notificationDelegate;
                    userControl = testFormUserControl;
                    break;
                case "ServiceManager":
                    ServiceManagerUserControl serviceManagerUserControl = new ServiceManagerUserControl();
                    serviceManagerUserControl.PrintToConsoleEvent += printToConsole;
                    userControl = serviceManagerUserControl;
                    break;
                case "Application Manager":
                    PersonalManagerUserControl personalManagerUserControl = new PersonalManagerUserControl();
                    personalManagerUserControl.PrintToConsoleEvent += printToConsole;
                    userControl = personalManagerUserControl;
                    break;
                case "Notifications":
                    NotificationsUserControl notificationsUserControl = new NotificationsUserControl();
                    notificationsUserControl.PrintToConsoleEvent += printToConsole;
                    userControl = notificationsUserControl;
                    break;
                case "JSON Builder":
                    JSONUserControl jsonUserControl = new JSONUserControl();
                    jsonUserControl.PrintToConsoleEvent += printToConsole;
                    userControl = jsonUserControl;
                    break;
                default:
                    break;
            }

            UserControls.Add(text, userControl);
            return userControl;
        }
    }
}
