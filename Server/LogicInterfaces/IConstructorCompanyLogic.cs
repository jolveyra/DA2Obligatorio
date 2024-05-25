using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyLogic
    {
        ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany);
        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies();
        ConstructorCompany GetConstructorCompanyById(Guid guid);
    }
}