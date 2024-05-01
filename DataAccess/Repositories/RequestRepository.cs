using DataAccess.Context;
using Domain;
using Microsoft.EntityFrameworkCore;
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
            Flat? flat = _context.Flats.Find(request.Flat.Id);

            if(flat is null)
            {
                throw new ArgumentException("Flat not found");
            }

            request.Flat = flat;
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests.Include(r => r.Category).Include(r => r.Flat);
        }

        public Request GetRequestById(Guid id)
        {
            Request? request = _context.Requests.Include(r => r.Category).Include(r => r.Flat).FirstOrDefault(r => r.Id == id);

            if (request == null)
            {
                throw new ArgumentException("Request not found");
            }

            return request;
        }

        public Request UpdateRequest(Request existingRequest)
        {
            _context.Requests.Update(existingRequest);
            _context.SaveChanges();
            return existingRequest;
        }
    }
}
