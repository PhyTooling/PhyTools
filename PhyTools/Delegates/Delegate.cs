using PhyTools.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Delegates
{
    /// <summary>
    /// Delegate to print to console window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="printToConsoleEvent"></param>
    public delegate void PrintToConsole(object sender, PrintToConsoleEvent printToConsoleEvent);
}
