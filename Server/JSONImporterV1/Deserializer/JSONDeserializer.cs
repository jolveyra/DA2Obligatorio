using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONImporterV1.Deserializer
{
    public class JSONDeserializer: IJSONDeserializer
    {
        public T Deserialize<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}
