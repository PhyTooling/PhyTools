using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.TreeModels.TreeViewModel
{
    public class KeyArrayNodeTreeViewModel : TreeViewItemViewModel
    {
        private KeyArrayNode _KeyArrayNode;

        public KeyArrayNodeTreeViewModel(KeyArrayNode keyArrayNode)
            : base(null, true)
        {
            _KeyArrayNode = keyArrayNode;
            this.IsExpanded = true;
        }

        public string Value
        {
            get { return _KeyArrayNode.Key as string; }
        }

        protected override void LoadChildren()
        {
            var nodes = _KeyArrayNode.Values;

            foreach(var node in nodes)
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

        protected override void AddBranchMethodCommand()
        {
        }

        protected override void AddArrayMethodCommand()
        {
        }

        protected override void AddDictionaryMethodCommand()
        {
        }

        protected override void DeleteMethodCommand()
        {
            _KeyArrayNode = null;
        }
    }
}
