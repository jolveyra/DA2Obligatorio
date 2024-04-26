using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class RequestLogic : IRequestLogic
    {
        private readonly IRequestRepository _requestRepository;

        public RequestLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public Request CreateRequest(Request request)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Request> GetAllRequests()
        {
            return _requestRepository.GetAllRequests();
        }

        public Request GetRequestById(Guid id)
        {
            return _requestRepository.GetRequestById(id);
        }

        public Request UpdateRequest(Request request)
        {
            Request existingRequest = GetRequestById(request.Id);

            existingRequest.AssignedEmployeeId = request.AssignedEmployeeId;
            existingRequest.BuildingId = request.BuildingId;
            existingRequest.FlatId = request.FlatId;
            existingRequest.Category = request.Category;
            existingRequest.Description = request.Description;

            return _requestRepository.UpdateRequest(existingRequest);
        }
    }
}
