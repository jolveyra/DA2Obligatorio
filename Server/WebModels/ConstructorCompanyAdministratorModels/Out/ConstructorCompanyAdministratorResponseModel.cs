using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using WebModels.ConstructorCompanyModels;

namespace WebModels.ConstructorCompanyAdministratorModels
{
    public class ConstructorCompanyAdministratorResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public Guid ConstructorCompanyId { get; set; }

        public ConstructorCompanyAdministratorResponseModel(ConstructorCompanyAdministrator admin)
        {
            Id = admin.Id;
            Name = admin.Name;
            Surname = admin.Surname;
            Email = admin.Email;
            ConstructorCompanyId = admin.ConstructorCompany.Id;
        }

        public override bool Equals(object? obj)
        {
            return obj is ConstructorCompanyAdministratorResponseModel constructorCompanyAdministratorResponseModel && Id == constructorCompanyAdministratorResponseModel.Id;
        }
    }
}
