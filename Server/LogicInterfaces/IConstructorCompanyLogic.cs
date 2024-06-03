using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyLogic
    {
        ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany, Guid administratorId);
        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies();
        ConstructorCompany GetConstructorCompanyById(Guid guid);
        ConstructorCompany UpdateConstructorCompany(string newName, Guid userId, Guid constructoCompanyId);
    }
}