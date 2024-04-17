using Domain;

namespace LogicInterfaces
{
    public interface IMaintenanceEmployeeLogic
    {
        User CreateMaintenanceEmployee(User user);
        IEnumerable<User> GetAllMaintenanceEmployees();
    }
}
