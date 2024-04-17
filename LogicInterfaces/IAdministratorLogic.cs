
using Domain;

namespace LogicInterfaces
{
    public interface IAdministratorLogic
    {
        User CreateAdministrator(User user);
        IEnumerable<User> GetAllAdministrators();
    }
}
