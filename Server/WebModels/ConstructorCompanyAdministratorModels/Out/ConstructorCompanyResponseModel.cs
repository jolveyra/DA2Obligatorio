using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace WebModels.ConstructorCompanyAdministratorModels
{
    public class ConstructorCompanyResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public ConstructorCompanyResponseModel(ConstructorCompany constructorCompany)
        {
            Id = constructorCompany.Id;
            Name = constructorCompany.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is ConstructorCompanyResponseModel constructorCompanyResponseModel && Id == constructorCompanyResponseModel.Id;
        }
    }
}
