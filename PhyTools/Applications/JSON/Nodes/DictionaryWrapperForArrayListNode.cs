using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Dictionary : {Key}")]
    public class DictionaryWrapperForArrayListNode : BasicNode
    {
        public string Key { get; set; }

        public string Index { get; set; }

        public DictionaryWrapperForArrayListNode(string key) : base()
        {
            Key = key;
        }
    }
}
