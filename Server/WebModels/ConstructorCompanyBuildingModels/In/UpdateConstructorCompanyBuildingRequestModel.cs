using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WebModels.ConstructorCompanyBuildingModels
{
    public class UpdateConstructorCompanyBuildingRequestModel
    {
        public string Name { get; set; }
        public Guid ManagerId { get; set; }

        public Building ToEntity()
        {
            return new Building { Name = Name, Manager = new User() { Id = ManagerId } };
        }
    }
}
