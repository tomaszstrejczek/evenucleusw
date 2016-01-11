using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace ts.staticcontent
{
    public static class TypeFromYaml
    {
        private static Dictionary<long, string> typeids;
        static TypeFromYaml()
        {
            var stream = typeof (TypeFromYaml).Assembly.GetManifestResourceStream("ts.staticcontent.res.typeids.bin");
            var formatter = new BinaryFormatter();
            typeids = (Dictionary<long, string>) formatter.Deserialize(stream);
        }

        public static string FromTypeId(long id)
        {
            string result;
            if (typeids.TryGetValue(id, out result))
                return result;
            else
                return "Unknown";
        }

        public static long FromTypeName(string name)
        {
            var namel = name.ToLower();
            foreach(var k in typeids)
                if (k.Value!=null && k.Value.ToLower() == namel)
                    return k.Key;

            return -1;
        }
    }
}
