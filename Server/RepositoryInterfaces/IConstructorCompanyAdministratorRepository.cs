using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyAdministratorRepository
    {
        ConstructorCompanyAdministrator CreateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
        IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators();
        ConstructorCompanyAdministrator GetConstructorCompanyAdministratorByUserId(Guid userId);
        ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
    }
}