using CustomExceptions.BusinessLogic;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class ReportLogic
    {
        private readonly IRequestRepository _requestRepository;

        public ReportLogic(IRequestRepository requestRepository)
        {
            _requestRepository = requestRepository;
        }

        public List<(string, int, int, int, double)> GetReport(string filter)
        {
            if (filter.ToLower().Equals("building"))
            {
                return GenerateReport(_requestRepository, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                return GenerateReport(_requestRepository, new EmployeeRequestReport());
            }

            throw new ReportException("Invalid filter");
        }

        private static List<(string, int, int, int, double)> GenerateReport(IRequestRepository _requestRepository, RequestReport requestReport)
        {
            return requestReport.GenerateReport(_requestRepository.GetAllRequests().ToList());
        }
    }
}
