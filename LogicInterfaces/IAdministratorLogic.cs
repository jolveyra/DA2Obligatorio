using Domain;

namespace LogicInterfaces
{
    public interface IAdministratorLogic
    {
        IEnumerable<User> GetAllAdministrators();
    }
}
