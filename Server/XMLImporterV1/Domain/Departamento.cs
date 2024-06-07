using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1.Domain
{
    public class Departamento
    {
        [XmlElement("piso")]
        public int Piso { get; set; }

        [XmlElement("numero_puerta")]
        public int NumeroPuerta { get; set; }

        [XmlElement("habitaciones")]
        public int Habitaciones { get; set; }

        [XmlElement("conTerraza")]
        public bool ConTerraza { get; set; }

        [XmlElement("baños")]
        public int Banos { get; set; }

        [XmlElement("propietarioEmail")]
        public string PropietarioEmail { get; set; }
    }
}
