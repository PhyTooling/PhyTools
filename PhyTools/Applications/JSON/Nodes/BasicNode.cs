using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("BasicNode")]
    public abstract class BasicNode
    {
        public List<BasicNode> Nodes = new List<BasicNode>();
    }
}