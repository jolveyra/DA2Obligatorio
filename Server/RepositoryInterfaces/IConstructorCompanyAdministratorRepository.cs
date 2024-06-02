using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyAdministratorRepository
    {
        ConstructorCompanyAdministrator CreateConstructorCompanyAdministrator(User user);
        IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators();
        ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator);
    }
}