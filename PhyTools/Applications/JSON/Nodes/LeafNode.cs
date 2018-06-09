using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Leaf : {Value}")]
    public class LeafNode : BasicNode
    {
        public object Value { get; set; }

        public LeafNode(object value) : base()
        {
            Value = value;
        }
    }
}
