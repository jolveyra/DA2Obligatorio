using Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebModels.ImporterModels
{
    public class CreateImporterRequestModel
    {
        public string Name { get; set; }

        public Importer ToEntity()
        {
            return new Importer { 
                Name = Name
            };
        }

    }
}
