using Domain;

namespace LogicInterfaces
{
    public interface IManagerRequestLogic
    {
        Request CreateRequest(Request request);
        IEnumerable<Request> GetAllManagerRequests(Guid managerId);
        Request GetRequestById(Guid id);
        Request UpdateRequest(Request request);
    }
}
