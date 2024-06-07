using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSONImporterV1
{
    public class DTOFlat
    {
        public int Number { get; set; }
        public int Floor { get; set; }
        public string OwnerEmail { get; set; }
        public int Rooms { get; set; }
        public int Bathrooms { get; set; }
        public bool HasBalcony { get; set; } = false;
    }
}
