using CustomExceptions;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class RequestLogic : IManagerRequestLogic, IEmployeeRequestLogic
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IBuildingRepository _buildingRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IUserRepository _userRepository;

        public RequestLogic(IRequestRepository requestRepository, IBuildingRepository buildingRepository, IUserRepository userRepository, ICategoryRepository categoryRepository)
        {
            _requestRepository = requestRepository;
            _buildingRepository = buildingRepository;
            _userRepository = userRepository;
            _categoryRepository = categoryRepository;
        }

        public Request CreateRequest(Request request, Guid userId)
        {
            ValidateRequest(request);
            ValidateCategory(request);
            request.ManagerId = userId;
            request.Flat = _buildingRepository.GetFlatByFlatId(request.Flat.Id);
            request.Building = request.Flat.Building;
            request.AssignedEmployee = _userRepository.GetUserById(request.AssignedEmployee.Id);
            request.Status = RequestStatus.Pending;

            return _requestRepository.CreateRequest(request);
        }

        public IEnumerable<Request> GetAllManagerRequests(Guid userId)
        {
            return _requestRepository.GetAllRequests().Where(r => r.ManagerId == userId);
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

            if (existingRequest.Status == RequestStatus.Completed)
                throw new RequestException("Cannot update a completed request");
            
            ValidateRequest(existingRequest);
            ValidateCategory(request);

            User maintenanceEmployee = _userRepository.GetUserById(request.AssignedEmployee.Id);

            existingRequest.AssignedEmployee = maintenanceEmployee;
            existingRequest.Category = request.Category;
            existingRequest.Description = request.Description;

            return _requestRepository.UpdateRequest(existingRequest);
        }

        public Request UpdateRequestStatusById(Guid requestId, RequestStatus requestStatus)
        {
            Request request = GetRequestById(requestId);
            request.Status = requestStatus;

            if (requestStatus == RequestStatus.Pending || requestStatus == RequestStatus.InProgress)
            {
                request.StartingDate = DateTime.Now;
                request.CompletionDate = DateTime.Now;
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

        private void ValidateCategory(Request request)
        {
            Category? category = _categoryRepository.GetAllCategories().FirstOrDefault(c => c.Name == request.Category.Name);
            if (category is null)
            {
                throw new RequestException("Category does not exist");
            }
            request.Category = category;
        }
    }
}
