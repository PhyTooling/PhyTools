using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.TreeModels.TreeViewModel
{
    public class ValueNodeTreeViewModel : TreeViewItemViewModel
    {
        private ValueNode _ValueNode;

        public ValueNodeTreeViewModel(ValueNode valueNode)
            : base(null, true)
        {
            _ValueNode = valueNode;
        }

        public string Value
        {
            get
            {
                if (_ValueNode.Value != null)
                {
                    return _ValueNode.Value.ToString();
                }
                else
                {
                    return _ValueNode.Value as string;
                }
            }
            set
            {
                _ValueNode.Value = value;
            }
        }

        protected override void LoadChildren()
        {
            // value nodes dont have children
        }
    }
}
