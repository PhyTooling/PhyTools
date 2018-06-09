using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSON.Nodes
{
    [DebuggerDisplay("Dictionary : {Key}")]
    public class DictionaryNode : BasicNode
    {
        public string Key { get; set; }

        public DictionaryNode(string key) : base()
        {
            Key = key;
        }
    }
}
