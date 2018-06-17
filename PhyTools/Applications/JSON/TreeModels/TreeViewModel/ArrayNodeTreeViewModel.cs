using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.TreeModels.TreeViewModel
{
    public class ArrayNodeTreeViewModel : TreeViewItemViewModel
    {
        private ArrayNode _ArrayNode;

        public ArrayNodeTreeViewModel(ArrayNode arrayNode)
            : base(null, true)
        {
            _ArrayNode = arrayNode;
            this.IsExpanded = true;
        }

        public string Value
        {
            get { return string.Format("[{0}]", _ArrayNode.Index); }
        }

        protected override void LoadChildren()
        {
            var nodes = _ArrayNode.Values;

            foreach (var node in nodes)
            {
                if (node is ArrayNode)
                {
                    var arrayNode = node as ArrayNode;
                    base.Children.Add(new ArrayNodeTreeViewModel(arrayNode));
                }
                else if (node is KeyArrayNode)
                {
                    var keyArrayNode = node as KeyArrayNode;
                    base.Children.Add(new KeyArrayNodeTreeViewModel(keyArrayNode));
                }
                else if (node is KeyValueNode)
                {
                    var keyValueNode = node as KeyValueNode;
                    base.Children.Add(new KeyValueNodeTreeViewModel(keyValueNode));
                }
                else if (node is ListKeyValueNode)
                {
                    var listKeyValueNode = node as ListKeyValueNode;
                    base.Children.Add(new ListKeyValueNodeTreeViewModel(listKeyValueNode));
                }
                else if (node is ValueNode)
                {
                    var valueNode = node as ValueNode;
                    base.Children.Add(new ValueNodeTreeViewModel(valueNode));
                }
            }
        }
    }
}
