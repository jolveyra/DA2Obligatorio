using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class RequestRepository : IRequestRepository
    {
        private readonly BuildingBossContext _context;

        public RequestRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public Request CreateRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests;
        }

        public Request GetRequestById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Request UpdateRequest(Request existingRequest)
        {
            throw new NotImplementedException();
        }
    }
}
