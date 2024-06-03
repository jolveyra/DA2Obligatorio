using Domain;

namespace WebModels.ConstructorCompanyModels
{
    public class ConstructorCompanyResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

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