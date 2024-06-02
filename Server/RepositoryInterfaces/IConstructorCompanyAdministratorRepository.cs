using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyAdministratorRepository
    {
        ConstructorCompanyAdministrator CreateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
        IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators();
        ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
    }
}