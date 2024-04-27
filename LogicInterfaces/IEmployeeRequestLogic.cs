using Domain;

namespace LogicInterfaces
{
    public interface IEmployeeRequestLogic
    {
        IEnumerable<Request> GetAllRequestsByEmployeeId(Guid userId);
    }
}
