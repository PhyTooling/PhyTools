using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Root")]
    public class RootNode : BasicNode
    {
        public string OriginalJson { get; set; }
        public Dictionary<string, object> OriginalDictionary { get; set; }
        public object Value { get; set; }
    }
}
