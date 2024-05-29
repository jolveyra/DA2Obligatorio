
using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyAdministratorLogic
    {
        ConstructorCompany GetAdminConstructorCompany(Guid userId);
        ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId);
    }
}