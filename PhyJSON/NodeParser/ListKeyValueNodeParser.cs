using PhyJSON.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.NodeParser
{
    public class ListKeyValueNodeParser
    {
        public ListKeyValueNode GetKeyValueNodes(Dictionary<string, object> dictionary, int index)
        {
            List<INode> values = new List<INode>();
            foreach (var keyValuePair in dictionary)
            {
                if (keyValuePair.Value is ArrayList)
                {
                    var keyArrayNodeParser = new KeyArrayNodeParser();
                    var arrayNode = keyArrayNodeParser.GetArrayListNode(keyValuePair.Key, keyValuePair.Value as ArrayList);
                    values.Add(arrayNode);
                }
                else
                {
                    var keyValueNodeParser = new KeyValueNodeParser();
                    var keyValueNode = keyValueNodeParser.GetKeyValueNode(keyValuePair);
                    values.Add(keyValueNode);
                }
            }

            ListKeyValueNode listKeyvalueNode = new ListKeyValueNode();
            listKeyvalueNode.Index = index;
            listKeyvalueNode.Values = values;
            return listKeyvalueNode;
        }

        public Dictionary<string, object> GetDictionary(ListKeyValueNode listKeyValueNode)
        {
            List<KeyValuePair<string, object>> values = new List<KeyValuePair<string, object>>();

            foreach (var node in listKeyValueNode.Values)
            {
                if (node is KeyArrayNode)
                {
                    var keyArrayNodeParser = new KeyArrayNodeParser();
                    var keyValuePair = keyArrayNodeParser.GetKeyArrayListPair(node as KeyArrayNode);
                    values.Add(new KeyValuePair<string, object>(keyValuePair.Key, keyValuePair.Value));
                }
                else
                {
                    var keyValueNodeParser = new KeyValueNodeParser();
                    var keyValuePair = keyValueNodeParser.GetKeyValuePair(node as KeyValueNode);
                    values.Add(keyValuePair);
                }
            }

            Dictionary<string, object> dictionary = new Dictionary<string, object>();
            foreach (var value in values)
            {
                dictionary.Add(value.Key, value.Value);
            }

            return dictionary;
        }
    }
}
