using CustomExceptions.BusinessLogic;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IRequestRepository _requestRepository;
        private readonly IUserRepository _userRepository;

        public ReportLogic(IRequestRepository requestRepository, IUserRepository userRepository)
        {
            _requestRepository = requestRepository;
            _userRepository = userRepository;
        }

        public IEnumerable<(string, int, int, int, double)> GetReport(Guid managerId, string filter)
        {
            User manager = _userRepository.GetUserById(managerId);

            if (filter.ToLower().Equals("building"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.Flat.Building.Manager.Equals(manager));
                return GenerateReport(requests, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.Flat.Building.Manager.Equals(manager));
                return GenerateReport(requests, new EmployeeRequestReport());
            }

            throw new ReportException("Invalid filter");
        }

        private static IEnumerable<(string, int, int, int, double)> GenerateReport(IEnumerable<Request> requests, RequestReport requestReport)
        {

            return requestReport.GenerateReport(requests);
        }
    }
}
