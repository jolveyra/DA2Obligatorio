using Domain;

namespace LogicInterfaces
{
    public interface IMaintenanceEmployeeLogic
    {
        IEnumerable<User> GetAllMaintenanceEmployees();
    }
}
