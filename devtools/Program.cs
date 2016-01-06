using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;

namespace devtools
{
    class Program
    {
        static void Generate1(StreamWriter writer, List<Tuple<long, string>> typeids)
        {
            writer.WriteLine(@"
                using System.Collections.Generic;
                namespace ts.staticcontent
                {
	                public static class TypeFromYaml
	                {
		                static private var dict = new Dictionary<long, string>( {
                ");

            foreach (var t in typeids)
            {
                if (t != null && t.Item2 != null)
                {
                    writer.WriteLine($"{{ {t.Item1}, @\"{t.Item2.Replace('"', '\'')}\"}},");
                }
            }

            writer.WriteLine(@"				});
		            public static string FromTypeId(long typeid)
		            {
			            string result;
			            if (dict.TryGetValue(typeid, out result))
				            return result;
			            else
				            return ""Unknown"";

                    }
                }
                }");
        }

        static void Generate2(StreamWriter writer, List<Tuple<long, string>> typeids)
        {
            writer.WriteLine(@"
                namespace ts.staticcontent
                {
	                public static class TypeFromYaml
	                {
                ");

            writer.WriteLine(@"
		            public static string FromTypeId(long typeid)
		            {
			            switch(typeid) { 
            ");


            foreach (var t in typeids)
            {
                if (t != null && t.Item2 != null)
                {
                    writer.WriteLine($"case {t.Item1}: return @\"{t.Item2.Replace('"', '\'')}\";");
                }
            }


            writer.WriteLine(@" default: return ""Unknown"";
                    }
                }
                }
            }");
        }

        static void Main(string[] args)
        {
            string codeBase = Assembly.GetExecutingAssembly().CodeBase;
            UriBuilder uri = new UriBuilder(codeBase);
            string path = Uri.UnescapeDataString(uri.Path);
            var curDir = Path.GetDirectoryName(path);
            var solutionDir = Path.Combine(curDir, "../../..");

            var inputFile = Path.Combine(solutionDir, @"staticcontent\static\typeIDs.yaml.zlib");
            var outputFile = Path.Combine(solutionDir, @"staticcontent\TypesFromYaml.cs");
            var outputResFile = Path.Combine(solutionDir, @"staticcontent\res\typeids.bin");
            var test = new TypeIdsYaml();
            var typeids = test.ReadCompressedFile(@"C:\w.evenucleusw\staticcontent\static\typeIDs.yaml.zlib");

            //using (var writer = new StreamWriter(outputFile))
            //{
            //    //Generate3(writer, typeids);
            //}

            using (var stream = new FileStream(outputResFile, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                var dict = new Dictionary<long, string>(typeids.ToDictionary(key => key.Item1, value => value.Item2));
                var formatter = new BinaryFormatter();
                formatter.Serialize(stream, dict);
            }
        }
    }
}
