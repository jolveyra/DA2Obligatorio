using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyAdministratorRepository
    {
        ConstructorCompanyAdministrator GetConstructorCompanyAdministratorById(Guid userId);
        ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
    }
}