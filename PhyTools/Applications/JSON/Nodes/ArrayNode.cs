using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Array : {Key}")]
    public class ArrayNode : BasicNode
    {
        public string Key { get; set; }

        public ArrayNode(string key) : base()
        {
            Key = key;
        }
    }
}
