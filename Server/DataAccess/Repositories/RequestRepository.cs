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
            _context.Requests.Add(request);
            _context.SaveChanges();
            return request;
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _context.Requests
                .Include(r => r.Category)
                .Include(r => r.Flat)
                .Include(r => r.Building)
                .Include(r => r.Building.Address)
                .Include(r => r.AssignedEmployee);

        }

        public Request GetRequestById(Guid id)
        {
            Request? request = _context.Requests
                .Include(r => r.Category)
                .Include(r => r.Flat)
                .Include(r => r.Building)
                .Include(r => r.Building.Address)
                .Include(r => r.AssignedEmployee)
                .FirstOrDefault(r => r.Id == id);

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

        public IEnumerable<Request> GetAllRequestsWithBuilding()
        {
            return _context.Requests.Include(r => r.Flat.Building);
        }
    }
}
