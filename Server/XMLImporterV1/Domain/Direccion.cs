using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1.Domain
{
    public class Direccion
    {
        [XmlElement("calle_principal")]
        public string CallePrincipal { get; set; }

        [XmlElement("numero_puerta")]
        public int NumeroPuerta { get; set; }

        [XmlElement("calle_secundaria")]
        public string CalleSecundaria { get; set; }
    }
}
