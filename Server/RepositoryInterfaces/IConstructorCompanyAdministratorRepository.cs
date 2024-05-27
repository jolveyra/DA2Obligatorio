
using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyAdministratorRepository
    {
        ConstructorCompanyAdministrator GetConstructorCompanyAdministratorByUserId(Guid guid);
    }
}