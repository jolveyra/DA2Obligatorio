
using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyAdministratorLogic
    {
        ConstructorCompanyAdministrator GetConstructorCompanyAdministrator(Guid userId);
        ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId);
    }
}