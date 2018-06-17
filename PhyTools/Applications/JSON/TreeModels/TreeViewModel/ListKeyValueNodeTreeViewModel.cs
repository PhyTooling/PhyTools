using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.TreeModels.TreeViewModel
{
    public class ListKeyValueNodeTreeViewModel : TreeViewItemViewModel
    {
        private ListKeyValueNode _ListKeyValueNode;

        public ListKeyValueNodeTreeViewModel(ListKeyValueNode listKeyValueNode)
            : base(null, true)
        {
            _ListKeyValueNode = listKeyValueNode;
            this.IsExpanded = true;
        }

        public string Value
        {
            get { return string.Format("[{0}]", _ListKeyValueNode.Index); }
        }

        protected override void LoadChildren()
        {
            var nodes = _ListKeyValueNode.Values;

            foreach (var node in nodes)
            {
                if (node is KeyArrayNode)
                {
                    base.Children.Add(new KeyArrayNodeTreeViewModel(node as KeyArrayNode));
                }
                else if (node is KeyValueNode)
                {
                    base.Children.Add(new KeyValueNodeTreeViewModel(node as KeyValueNode));
                }
                else
                {
                    throw new Exception();
                }
            }
        }
    }
}

