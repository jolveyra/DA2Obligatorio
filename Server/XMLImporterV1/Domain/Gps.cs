using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1.Domain
{
    public class Gps
    {
        [XmlElement("latitud")]
        public double Latitud { get; set; }

        [XmlElement("longitud")]
        public double Longitud { get; set; }
    }
}
