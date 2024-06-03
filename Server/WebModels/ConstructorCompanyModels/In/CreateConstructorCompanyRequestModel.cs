using Domain;

namespace WebModels.ConstructorCompanyModels
{
    public class CreateConstructorCompanyRequestModel
    {
        public string Name { get; set; }

        public ConstructorCompany ToEntity()
        {
            return new ConstructorCompany { Name = Name };
        }
    }
}