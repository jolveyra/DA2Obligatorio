using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONImporterV1.Domain
{    
    public class Departamento
    {
        public int Piso { get; set; }
        public int Numero_puerta { get; set; }
        public int Habitaciones { get; set; }
        public bool ConTerraza { get; set; }
        public int Baños { get; set; }
        public string PropietarioEmail { get; set; }
    }
}
