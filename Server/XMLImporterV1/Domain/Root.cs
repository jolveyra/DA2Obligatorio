using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1.Domain
{
    [XmlRoot("root")]
    public class Root
    {
        [XmlElement("edificios")]
        public List<Edificio> Edificios { get; set; }
    }
}
