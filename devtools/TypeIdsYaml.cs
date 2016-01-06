using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Compression;
using System.IO;
using System.Net;
using YamlDotNet.Dynamic;
using YamlDotNet.RepresentationModel;
using YamlDotNet.Serialization;

namespace devtools
{
    public class TypeIdsYaml
    {
        // File typeIDs compressed with zopfli --deflate
        // The static dump got from https://developers.eveonline.com/resource/resources
        public List<Tuple<long, string>> ReadCompressedFile(string path)
        {
            var result = new List<Tuple<long, string>>();

            using (var file = new FileStream(path, FileMode.Open))
            {
                using (var zipfile = new DeflateStream(file, CompressionMode.Decompress))
                {
                    using (var textReader = new StreamReader(zipfile))
                    {
                        var yaml = new YamlStream();
                        yaml.Load(textReader);

                        // Examine the stream
                        var mapping =
                            (YamlMappingNode)yaml.Documents[0].RootNode;

                        foreach (var entry in mapping.Children)
                        {
                            dynamic en = new DynamicYaml(entry.Value);
                            //Console.WriteLine(((YamlScalarNode)entry.Key).Value);
                            string str = en.name.en;
                            result.Add(new Tuple<long, string>(long.Parse(((YamlScalarNode)entry.Key).Value), str));
                        }

                    }
                }
            }

            return result;
        }
    }
}
