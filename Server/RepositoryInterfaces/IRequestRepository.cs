using Domain;

namespace RepositoryInterfaces
{
    public interface IRequestRepository
    {
        Request CreateRequest(Request request);
        public IEnumerable<Request> GetAllRequests();
        public IEnumerable<Request> GetAllRequestsWithBuilding();
        public Request GetRequestById(Guid id);
        public Request UpdateRequest(Request existingRequest);

    }
}
