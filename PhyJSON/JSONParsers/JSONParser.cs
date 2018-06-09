using Newtonsoft.Json;
using PhyJSON.NodeParser;
using PhyJSON.Nodes;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PhyJSON.JSONParsers
{
    /// <summary>
    /// Parse object into JSON
    /// </summary>
    public class JSONParser
    {
        /// <summary>
        /// Parse object to JSON string
        /// </summary>
        /// <param name="objectToParse"></param>
        /// <returns></returns>
        public string ParseToJSONString(object objectToParse)
        {
            string json = string.Empty;
            JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
            try
            {
                // now we got the JSON
                json = JsonConvert.SerializeObject(objectToParse);
            }
            catch (Exception exception)
            {

            }
            return json;
        }

        /// <summary>
        /// Parse a json string into a dictionary
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public Dictionary<string, object> ParseToDictionary(string json)
        {
            try
            {
                JavaScriptSerializer js = new JavaScriptSerializer();
                Dictionary<string, object> dictionary = js.Deserialize<Dictionary<string, object>>(json);
                return dictionary;
            }
            catch (Exception exception)
            {

            }
            return null;
        }

        /// <summary>
        /// Parse a dictionary to a KeyValueNode
        /// </summary>
        /// <param name="jsonAsDictionary"></param>
        /// <returns></returns>
        public KeyValueNode ParseToKeyValueNode(Dictionary<string, object> jsonAsDictionary)
        {
            KeyValueNode rootnode = new KeyValueNode()
            {
                Key = "Root",
            };

            var listKeyValueNodeParser = new ListKeyValueNodeParser();
            var listKeyValueNode = listKeyValueNodeParser.GetKeyValueNodes(jsonAsDictionary, 0);

            rootnode.Value = listKeyValueNode;

            return rootnode;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="rootNode"></param>
        /// <returns></returns>
        public Dictionary<string, object> ParseToDictionary(KeyValueNode rootNode)
        {
            var listKeyValueNodeParser = new ListKeyValueNodeParser();
            var jsonAsDictionary = listKeyValueNodeParser.GetDictionary(rootNode.Value as ListKeyValueNode);

            return jsonAsDictionary;
        }
    }
}
