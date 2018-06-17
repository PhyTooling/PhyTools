using PhyJSON.JSONParsers;
using PhyJSON.Nodes;
using PHyJSONWinforms.JSONManagers.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace PHyJSONWinforms.JSONManagers
{
    public class JSONManager
    {
        private JSONParser JSONParser = new JSONParser();

        public KeyValueNode GetKeyValueNode()
        {
            #region some random object to parse
            var dictionaryTest = new Dictionary<object, object>();
            dictionaryTest.Add(new Car(), "A-VALUE");
            dictionaryTest.Add("B-KEY", "B-VALUE");

            var test2 = new List<object>()
            {
                1,
                2,
            };

            var test = new List<object>()
            {
                dictionaryTest,
                1,
                test2,
                2
            };

            JSONTestObject jsonTestObject = new JSONTestObject()
            {
                Name = "test",
                Cars = new List<object>()
                {
                    test,
                    (new Car3() { CarNames = new List<object> () { new Car() { CarName = "3456" }, "1234" }, CarAge2 = "6666" }) as object,
                    (new Car2() { CarName2 = "5555", CarAge2 = "6666" }) as object,
                    (new Car() { CarName = "2" }) as object,
                    (1),
                },
                MyDictionary = dictionaryTest,
            };
            #endregion

            #region turn Object to JSON string
            string json = JSONParser.ParseToJSONString(jsonTestObject as object);
            #endregion

            Dictionary<string, object> jsonAsdictionary = JSONParser.ParseToDictionary(json);

            #region turn json into BasicNode
            // turn into a tree of basic nodes
            KeyValueNode rootNode = JSONParser.ParseToKeyValueNode(jsonAsdictionary);
            #endregion


            #region turn the BasicRoot back to JSON for validation
            // check if you can turn it back into a json
            var keyValueNodeAsDictionary = JSONParser.ParseToDictionary(rootNode);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string keyValueNodeAsJSON = js.Serialize(keyValueNodeAsDictionary);

            if (keyValueNodeAsJSON != json)
            {
                throw new Exception("failed to deserialise");
            }
            int x = 0;

            return rootNode;
            #endregion
        }

        public string ParseToJSON(KeyValueNode keyValueNode)
        {
            // check if you can turn it back into a json
            var keyValueNodeAsDictionary = JSONParser.ParseToDictionary(keyValueNode);

            JavaScriptSerializer js = new JavaScriptSerializer();
            string keyValueNodeAsJSON = js.Serialize(keyValueNodeAsDictionary);

            return keyValueNodeAsJSON;
        }
    }
}
