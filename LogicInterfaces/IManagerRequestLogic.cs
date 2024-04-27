using Domain;

namespace LogicInterfaces
{
    public interface IManagerRequestLogic
    {
        Request CreateRequest(Request request);
        IEnumerable<Request> GetAllRequests();
        Request GetRequestById(Guid id);
        Request UpdateRequest(Request request);
    }
}
