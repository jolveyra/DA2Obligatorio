using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONImporterV1.Domain
{
    public class Edificio
    {
        public string Nombre { get; set; }
        public Direccion Direccion { get; set; }
        public string Encargado { get; set; }
        public Gps Gps { get; set; }
        public float GastosComunes { get; set; }
        public List<Departamento> Departamentos { get; set; }
    }
}
