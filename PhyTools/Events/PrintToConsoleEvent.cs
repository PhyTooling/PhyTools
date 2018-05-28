using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Events
{
    /// <summary>
    /// Print to console event
    /// </summary>
    public class PrintToConsoleEvent : EventArgs
    {
        /// <summary>
        /// Text to print
        /// </summary>
        public string Text { get; set; }
    }
}
