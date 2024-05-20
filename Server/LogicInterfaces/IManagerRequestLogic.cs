using Domain;

namespace LogicInterfaces
{
    public interface IManagerRequestLogic
    {
        Request CreateRequest(Request request, Guid userId);
        IEnumerable<Request> GetAllManagerRequests(Guid managerId);
        Request GetRequestById(Guid id);
        Request UpdateRequest(Request request);
    }
}
