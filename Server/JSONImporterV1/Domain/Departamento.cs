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
        public int NumeroPuerta { get; set; }
        public int Habitaciones { get; set; }
        public bool ConTerraza { get; set; }
        public int Banos { get; set; }
        public string PropietarioEmail { get; set; }
    }
}
