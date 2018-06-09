using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Branch : {Key}")]
    public class BranchNode : BasicNode
    {
        public string Key { get; set; }

        public bool IsIndex { get; set; }

        public BranchNode(string value) : base()
        {
            Key = value;
        }
    }
}
