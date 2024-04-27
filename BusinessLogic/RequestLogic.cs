using CustomExceptions.BusinessLogic;
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

            ValidateRequest(existingRequest);

            return _requestRepository.UpdateRequest(existingRequest);
        }

        private void ValidateRequest(Request request)
        {
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new RequestException("Description cannot be empty or null");
            }
            if (request.BuildingId == Guid.Empty)       // Deberiamos chequear que el id sea perteneciente a unos de los de manager??
            {
                throw new RequestException("BuildingId cannot be empty or null");
            }
            if (request.FlatId == Guid.Empty) // Lo mismo con el building
            {
                throw new RequestException("FlatId cannot be empty or null");
            }
            if (request.AssignedEmployeeId == Guid.Empty)
            {
                throw new RequestException("AssignedEmployeeId cannot be empty or null");
            }
            if (request.Category == null)
            {
                throw new RequestException("Category cannot be null");
            }
        }
    }
}
