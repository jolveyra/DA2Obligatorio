using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1
{
    public class XMLDeserializer : IXmlDeserializer
    {
        public T Deserialize<T>(string filePath)
        {
            XmlSerializer serializer = new XmlSerializer(typeof(T));
            using (FileStream fileStream = new FileStream(filePath, FileMode.Open))
            {
                return (T)serializer.Deserialize(fileStream);
            }
        }
    }
}
