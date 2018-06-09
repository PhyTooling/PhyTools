using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.Nodes
{
    public class ValueNode : INode
    {
        public object Value { get; set; }
    }
}
