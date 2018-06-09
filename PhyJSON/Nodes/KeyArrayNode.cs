using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.Nodes
{
    public class KeyArrayNode : INode
    {
        /// <summary>
        /// Key
        /// </summary>
        public string Key { get; set; }

        /// <summary>
        /// The items in the ArrayList
        /// </summary>
        public List<INode> Values = new List<INode>();
    }
}
