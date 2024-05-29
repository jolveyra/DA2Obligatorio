
using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyAdministratorLogic
    {
        ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId);
    }
}