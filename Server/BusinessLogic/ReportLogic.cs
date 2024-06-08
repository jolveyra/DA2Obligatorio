using CustomExceptions;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class ReportLogic : IReportLogic
    {
        private readonly IRequestRepository _requestRepository;

        public ReportLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public IEnumerable<Report> GetReport(Guid managerId, string filter)
        {
            if (filter.ToLower().Equals("building"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.ManagerId == managerId);
                return GenerateReport(requests, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.ManagerId == managerId);
                return GenerateReport(requests, new EmployeeRequestReport());
            }

            throw new ReportException("Invalid filter");
        }

        private static IEnumerable<Report> GenerateReport(IEnumerable<Request> requests, RequestReportTemplate requestReport)
        {
            return requestReport.GenerateReport(requests);
        }
    }
}
