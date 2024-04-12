using Domain;

namespace LogicInterfaces
{
    public interface IRequestLogic
    {
        IEnumerable<Request> GetAllRequests();
        Request GetRequestById(Guid id);
    }
}
