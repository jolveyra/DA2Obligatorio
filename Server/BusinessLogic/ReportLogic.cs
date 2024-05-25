using CustomExceptions;
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

        public IEnumerable<Report> GetReport(Guid managerId, string filter)
        {
            if (filter.ToLower().Equals("building"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequestsWithBuilding().Where(r => r.ManagerId == managerId);
                return GenerateReport(requests, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.ManagerId == managerId);
                List<Report> reports = GenerateReport(requests, new EmployeeRequestReport()).ToList();

                foreach (Report report in reports)
                {
                    User employee = _userRepository.GetUserById(Guid.Parse(report.Filter));
                    report.Filter = employee.Name + " " + employee.Surname;
                }

                return reports;
            }

            throw new ReportException("Invalid filter");
        }

        private static IEnumerable<Report> GenerateReport(IEnumerable<Request> requests, RequestReportTemplate requestReport)
        {
            return requestReport.GenerateReport(requests);
        }
    }
}
