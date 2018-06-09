using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.TreeModels.TreeViewModel
{
    public class KeyValueNodeContainerViewModel
    {
        readonly ReadOnlyCollection<KeyValueNodeTreeViewModel> _keyValueNode;

        public KeyValueNodeContainerViewModel(List<KeyValueNode> keyValueNodes)
        {
            _keyValueNode = new ReadOnlyCollection<KeyValueNodeTreeViewModel>(
                (from rootNode in keyValueNodes
                 select new KeyValueNodeTreeViewModel(rootNode))
                .ToList());
        }

        /// <summary>
        /// The name needs to match to the Binding itemSource on the TreeView
        /// </summary>
        public ReadOnlyCollection<KeyValueNodeTreeViewModel> RootTreeNodes
        {
            get { return _keyValueNode; }
        }
    }
}
