using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PHyJSONWinforms.JSONManagers.Models
{
    public class NewNode
    {
        public string Path
        {
            get
            {
                string path = "";
                bool previousNodeIsArray = false;
                foreach(var node in KeyNodes.Keys)
                {
                    if (node is ArrayNode)
                    {
                        var arrayNode = node as ArrayNode;
                        path += string.Format("[{0}]", arrayNode.Index);
                        previousNodeIsArray = true;
                    }
                    else if (node is KeyArrayNode)
                    {
                        var keyArrayNode = node as KeyArrayNode;
                        path += string.Format("{0}{1}", path == "" || previousNodeIsArray ? "" : ".", keyArrayNode.Key);
                        previousNodeIsArray = false;
                    }
                    else if (node is KeyValueNode)
                    {
                        var keyValueNode = node as KeyValueNode;
                        path += string.Format("{0}{1}", path == "" || previousNodeIsArray ? "" : ".", keyValueNode.Key);
                        previousNodeIsArray = false;
                    }
                    else if (node is ListKeyValueNode)
                    {
                        var listKeyValueNode = node as ListKeyValueNode;
                        path += string.Format("[{0}]", listKeyValueNode.Index);
                        previousNodeIsArray = true;
                    }
                }
                return path;
            }
        }

        /// <summary>
        /// Field ToDisplay
        /// </summary>
        public string Field
        {
            get
            {
                var latestNode = KeyNodes.Last().Key;
                if (latestNode == null)
                {
                    return string.Empty;
                }

                if (latestNode is ArrayNode)
                {
                    var arrayNode = latestNode as ArrayNode;
                    return arrayNode.Index.ToString();
                }
                else if (latestNode is KeyArrayNode)
                {
                    var keyArrayNode = latestNode as KeyArrayNode;
                    return keyArrayNode.Key;
                }
                else if (latestNode is KeyValueNode)
                {
                    var keyValueNode = latestNode as KeyValueNode;
                    return keyValueNode.Key;
                }
                else if (latestNode is ListKeyValueNode)
                {
                    var listKeyValueNode = latestNode as ListKeyValueNode;
                    return listKeyValueNode.Index.ToString();
                }
                else if (latestNode is ValueNode)
                {
                    var valueNode = latestNode as ValueNode;
                    //return valueNode.Value;
                }

                return "";
            }
            set
            {
                var latestNode = KeyNodes.Last().Key;
                if (latestNode == null)
                {
                    return;
                }

                if (latestNode is ArrayNode)
                {
                    //var arrayNode = Node as ArrayNode;
                    //return arrayNode.index.ToString();
                }
                else if (latestNode is KeyArrayNode)
                {
                    var keyArrayNode = latestNode as KeyArrayNode;
                    //return keyArrayNode.Key;
                    keyArrayNode.Key = value;
                }
                else if (latestNode is KeyValueNode)
                {
                    var keyValueNode = latestNode as KeyValueNode;
                    //return keyValueNode.Key;
                    keyValueNode.Key = value;
                }
                else if (latestNode is ListKeyValueNode)
                {
                    //var listKeyValueNode = Node as ListKeyValueNode;
                    //return listKeyValueNode.Index.ToString();
                }
                else if (latestNode is ValueNode)
                {
                    //var valueNode = Node as ValueNode;
                    //return valueNode.Value;
                }
            }
        }

        /// <summary>
        /// Value to Display
        /// </summary>
        public string Value
        {
            get
            {
                if (ValueNode is ValueNode)
                {
                    var valueNode = ValueNode as ValueNode;
                    if (valueNode.Value is string)
                    {
                        return valueNode.Value as string;
                    }
                    else if (valueNode.Value is int)
                    {
                        var x = (int)valueNode.Value;
                        return x.ToString();
                    }
                }
                return "";
            }
            set
            {
                //if (ValueNode is ArrayNode)
                //{
                //    //var arrayNode = Node as ArrayNode;
                //    //return arrayNode.index.ToString();
                //}
                //else if (ValueNode is KeyArrayNode)
                //{
                //    var keyArrayNode = ValueNode as KeyArrayNode;
                //    //return keyArrayNode.Key;
                //    keyArrayNode.Key = value;
                //}
                //else if (ValueNode is KeyValueNode)
                //{
                //    var keyValueNode = ValueNode as KeyValueNode;
                //    //return keyValueNode.Key;
                //    keyValueNode.Key = value;
                //}
                //else if (ValueNode is ListKeyValueNode)
                //{
                //    //var listKeyValueNode = Node as ListKeyValueNode;
                //    //return listKeyValueNode.Index.ToString();
                //}
                //else if (ValueNode is ValueNode)
                //{
                //    //var valueNode = Node as ValueNode;
                //    //return valueNode.Value;
                //}

                if (ValueNode is ValueNode)
                {
                    var valueNode = ValueNode as ValueNode;

                    if (valueNode.Value is string)
                    {
                        valueNode.Value = value;
                        LinkNode(valueNode);
                    }
                    else if (valueNode.Value is int)
                    {
                        int x;
                        if (int.TryParse(value, out x))
                        {
                            valueNode.Value = x;
                            LinkNode(valueNode);
                        }
                    }
                }
            }
        }

        public void LinkNode(INode node)
        {
            var latestNode = KeyNodes.Last().Key;
            if (latestNode == null)
            {
                return;
            }

            if (latestNode is ArrayNode)
            {
                var arrayNode = latestNode as ArrayNode;
                //arrayNode.Values
            }
            else if (latestNode is KeyArrayNode)
            {
                var keyArrayNode = latestNode as KeyArrayNode;
                //keyArrayNode.Values
            }
            else if (latestNode is KeyValueNode)
            {
                var keyValueNode = latestNode as KeyValueNode;
                if (keyValueNode.Value == null)
                {
                    keyValueNode.Value = node;
                }
            }
            else if (latestNode is ListKeyValueNode)
            {
                var listKeyValueNode = latestNode as ListKeyValueNode;
                if (listKeyValueNode.Values == null)
                {
                }
            }
        }

        /// <summary>
        /// originalNode
        /// </summary>
        private Dictionary<INode, string> KeyNodes = new Dictionary<INode, string>();

        /// <summary>
        /// ValueNode
        /// </summary>
        private INode ValueNode { get; set; }

        public void AddKeyNode(INode keyNode, string path)
        {
            KeyNodes.Add(keyNode, path);
        }


        public Dictionary<INode, string> GetNodes()
        {
            return KeyNodes;
        }

        public void SetValueNode(INode keyNode)
        {
            ValueNode = keyNode;
        }

        public bool HasValueNode
        {
            get
            {
                if (ValueNode == null)
                {
                    return false;
                }
                return true;
            }
        }
    }
}
