
using Domain;

namespace LogicInterfaces
{
    public interface IConstructorCompanyAdministratorLogic
    {
        IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators(Guid userId);
        ConstructorCompanyAdministrator GetConstructorCompanyAdministrator(Guid userId);
        ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId);
    }
}