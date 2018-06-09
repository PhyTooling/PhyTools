using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhyTools.Applications.JSONApplication
{
    public class JSONTestObject
    {
        public string Name { get; set; }
        public string Tom { get; set; }
        public List<object> Cars { get; set; }
        public Dictionary<object, object> MyDictionary { get; set; }
    }

    public class Car
    {
        public string CarName { get; set; }
    }

    public class Car2
    {
        public string CarName2 { get; set; }
        public string CarAge2 { get; set; }
    }

    public class Car3
    {
        public List<object> CarNames { get; set; }
        public string CarAge2 { get; set; }
    }
}
