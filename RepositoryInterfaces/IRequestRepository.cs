using Domain;

namespace RepositoryInterfaces
{
    public interface IRequestRepository
    {
        public IEnumerable<Request> GetAllRequests();
    }
}
