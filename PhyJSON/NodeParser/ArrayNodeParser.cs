using PhyJSON.Nodes;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyJSON.NodeParser
{
    public class ArrayNodeParser
    {
        public ArrayNode GetArrayNode(ArrayList arrayList)
        {
            List<INode> nodes = new List<INode>();
            int counter = 0;
            foreach (var item in arrayList)
            {
                if (item is Dictionary<string, object>)
                {
                    // has key, value
                    var listKeyvalueNodeParser = new ListKeyValueNodeParser();
                    var listKeyvalueNode = listKeyvalueNodeParser.GetKeyValueNodes(item as Dictionary<string, object>, counter);
                    nodes.Add(listKeyvalueNode);
                }
                else if (item is ArrayList)
                {
                    // array that does not have a key
                    var arrayNodeParser = new ArrayNodeParser();
                    var arrayNode = arrayNodeParser.GetArrayNode(item as ArrayList);
                    nodes.Add(arrayNode);
                }
                else if (item is object)
                {
                    var ValueNodeParser = new ValueNodeParser();
                    var valueNode = ValueNodeParser.GetValueNode(item as object);
                    nodes.Add(valueNode);
                }
                else
                {
                    throw new Exception();
                }

                counter++;
            }

            var ArrayNode = new ArrayNode()
            {
                Values = nodes,
            };

            return ArrayNode;
        }

        public ArrayList GetArrayList(ArrayNode arrayNode)
        {
            ArrayList arrayList = new ArrayList();
            foreach (var item in arrayNode.Values)
            {
                if (item is ListKeyValueNode)
                {
                    // has key, value
                    var listKeyvalueNodeParser = new ListKeyValueNodeParser();
                    var listKeyvalueNode = listKeyvalueNodeParser.GetDictionary(item as ListKeyValueNode);
                    arrayList.Add(listKeyvalueNode);
                }
                else if (item is ArrayNode)
                {
                    var arrayNodeParser = new ArrayNodeParser();
                    var innerArrayList = arrayNodeParser.GetArrayList(item as ArrayNode);
                    arrayList.Add(innerArrayList);
                }
                else if (item is ValueNode)
                {
                    var ValueNodeParser = new ValueNodeParser();
                    var value = ValueNodeParser.GetDictionaryValue(item as ValueNode);
                    arrayList.Add(value);
                }
                else
                {
                    throw new Exception();
                }
            }
            return arrayList;
        }
    }
}
