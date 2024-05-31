using Domain;

namespace WebModels.ConstructorCompanyModels
{
    public class UpdateConstructorCompanyRequestModel
    {
        public string Name { get; set; }

        public ConstructorCompany ToEntity()
        {
            return new ConstructorCompany()
            {
                Name = Name
            };
        }
    }
}