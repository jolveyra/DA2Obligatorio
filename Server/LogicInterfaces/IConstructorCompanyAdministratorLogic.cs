
using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyAdministratorLogic
    {
        IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators();
        ConstructorCompanyAdministrator GetConstructorCompanyAdministrator(Guid userId);
        ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId);
    }
}