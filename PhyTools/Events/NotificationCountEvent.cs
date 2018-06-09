using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Events
{
    /// <summary>
    /// NotificationEvent
    /// </summary>
    public class NotificationCountEvent : EventArgs
    {
        /// <summary>
        /// Text
        /// </summary>
        public string Text { get; set; }
        
        /// <summary>
        /// Increment Amount
        /// </summary>
        public int Amount { get; set; }
    }
}
