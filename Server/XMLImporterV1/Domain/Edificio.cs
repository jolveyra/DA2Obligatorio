using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace XMLImporterV1.Domain
{
    public class Edificio
    {
        [XmlElement("nombre")]
        public string Nombre { get; set; }

        [XmlElement("direccion")]
        public Direccion Direccion { get; set; }

        [XmlElement("encargado")]
        public string Encargado { get; set; }

        [XmlElement("gps")]
        public Gps Gps { get; set; }

        [XmlElement("gastos_comunes")]
        public float GastosComunes { get; set; }

        [XmlElement("departamentos")]
        public List<Departamento> Departamentos { get; set; }
    }
}
