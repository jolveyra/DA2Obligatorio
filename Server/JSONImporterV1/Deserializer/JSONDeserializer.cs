using System;
using System.IO;
using System.Text.Json;
using System.Threading.Tasks;

namespace JSONImporterV1.Deserializer
{
    public class JSONDeserializer : IJSONDeserializer
    {
        public T Deserialize<T>(string path)
        {
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<T>(json);
        }
    }
}