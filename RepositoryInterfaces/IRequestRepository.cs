using Domain;

namespace RepositoryInterfaces
{
    public interface IRequestRepository
    {
        public IEnumerable<Request> GetAllRequests();
        Request GetRequestById(Guid id);
    }
}
