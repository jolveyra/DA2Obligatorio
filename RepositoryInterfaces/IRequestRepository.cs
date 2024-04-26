using Domain;

namespace RepositoryInterfaces
{
    public interface IRequestRepository
    {
        public IEnumerable<Request> GetAllRequests();
        public Request GetRequestById(Guid id);
        public Request UpdateRequest(Request existingRequest);
    }
}
