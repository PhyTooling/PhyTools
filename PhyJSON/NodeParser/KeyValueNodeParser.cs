using PhyJSON.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.NodeParser
{
    public class KeyValueNodeParser
    {
        public INode GetKeyValueNode(KeyValuePair<string, object> keyValuePair)
        {
            INode value = null;

            if (keyValuePair.Value is Dictionary<string, object>)
            {
                // has key, value
                var listKeyvalueNodeParser = new ListKeyValueNodeParser();
                var listKeyvalueNode = listKeyvalueNodeParser.GetKeyValueNodes(keyValuePair.Value as Dictionary<string, object>, 0);
                value = listKeyvalueNode;
            }
            //else if (keyValuePair.Value is ArrayList)
            //{
            //    var keyArrayNodeParser = new KeyArrayNodeParser();
            //    var arrayNode = keyArrayNodeParser.GetArrayListNode(keyValuePair.Key, keyValuePair.Value as ArrayList);
            //    value = arrayNode;
            //}
            else if (keyValuePair.Value is object) // needs to be last
            {
                var ValueNodeParser = new ValueNodeParser();
                var valueNode = ValueNodeParser.GetValueNode(keyValuePair.Value as object);
                value = valueNode;
            }
           
            KeyValueNode rootnode = new KeyValueNode();
            rootnode.Key = keyValuePair.Key;
            rootnode.Value = value;

            return rootnode;
        }


        public KeyValuePair<string, object> GetKeyValuePair(KeyValueNode node)
        {
            object value = null;

            if (node.Value is ListKeyValueNode)
            {
                // has key, value
                var listKeyvalueNodeParser = new ListKeyValueNodeParser();
                Dictionary<string, object> dictionary = listKeyvalueNodeParser.GetDictionary(node.Value as ListKeyValueNode);
                value = dictionary;
            }
            //else if (node.Value is KeyArrayNode)
            //{
            //    var keyArrayNodeParser = new KeyArrayNodeParser();
            //    KeyValuePair<string, ArrayList> keyValueArrayPair = keyArrayNodeParser.GetKeyArrayListPair(node.Value as KeyArrayNode);
            //    value = keyValueArrayPair;
            //}
            else if (node.Value is ValueNode)
            {
                var ValueNodeParser = new ValueNodeParser();
                var valueNode = ValueNodeParser.GetDictionaryValue(node.Value as ValueNode);
                value = valueNode;
            }

            var keyArrayListPair = new KeyValuePair<string, object>(node.Key, value);

            return keyArrayListPair;
        }
    }
}
