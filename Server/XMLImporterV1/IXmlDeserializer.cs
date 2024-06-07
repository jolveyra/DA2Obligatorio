using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XMLImporterV1
{
    public interface IXmlDeserializer
    {
        T Deserialize<T>(string filePath);
    }
}
