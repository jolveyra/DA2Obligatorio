using CustomExceptions.BusinessLogic;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class RequestLogic : IManagerRequestLogic, IEmployeeRequestLogic
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;

        public RequestLogic(IRequestRepository requestRepository, IUserRepository userRepository)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }

        public Request CreateRequest(Request request)
        {
            ValidateRequest(request);

            return _requestRepository.CreateRequest(request);
        }

        public IEnumerable<Request> GetAllManagerRequests(Guid userId)
        {
            User manager = _userRepository.GetUserById(userId);
            return _requestRepository.GetAllRequests().Where(r => r.Flat.Building.Manager.Equals(manager));
        }

        public IEnumerable<Request> GetAllRequestsByEmployeeId(Guid userId)
        {
            return _requestRepository.GetAllRequests().Where(r => r.AssignedEmployee.Id == userId);
        }

        public Request GetRequestById(Guid id)
        {
            return _requestRepository.GetRequestById(id);
        }

        public Request UpdateRequest(Request request)
        {
            Request existingRequest = GetRequestById(request.Id);

            existingRequest.AssignedEmployee = request.AssignedEmployee;
            existingRequest.Flat = request.Flat;
            existingRequest.Category = request.Category;
            existingRequest.Description = request.Description;

            ValidateRequest(existingRequest);

            return _requestRepository.UpdateRequest(existingRequest);
        }

        public Request UpdateRequestStatusById(Guid requestId, RequestStatus requestStatus)
        {
            Request request = GetRequestById(requestId);
            request.Status = requestStatus;

            if (requestStatus == RequestStatus.InProgress)
            {
                request.StartingDate = DateTime.Now;
            }
            else if (requestStatus == RequestStatus.Completed)
            {
                request.CompletionDate = DateTime.Now;
            }

            ValidateRequest(request);

            return _requestRepository.UpdateRequest(request);
        }

        private void ValidateRequest(Request request)
        {
            if (string.IsNullOrEmpty(request.Description))
            {
                throw new RequestException("Description cannot be empty or null");
            }
            if (request.Flat == null)
            {
                throw new RequestException("Flat cannot be empty or null");
            }
            if (request.AssignedEmployee == null)
            {
                throw new RequestException("AssignedEmployee cannot be empty or null");
            }
            if (request.Category == null)
            {
                throw new RequestException("Category cannot be null");
            }
        }
    }
}
