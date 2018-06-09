using PhyJSON.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.NodeParser
{
    public class ValueNodeParser
    {
        public ValueNode GetValueNode(object value)
        {
            if (value is ArrayList)
            {
                throw new Exception();
            }

            ValueNode valueNode = new ValueNode();
            valueNode.Value = value;
            return valueNode;
        }

        public object GetDictionaryValue(ValueNode valueNode)
        {
            if (valueNode.Value is ArrayList)
            {
                throw new Exception();
            }

            return valueNode.Value;
        }
    }
}
