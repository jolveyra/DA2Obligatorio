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

        public IEnumerable<(string, int, int, int, double)> GetReport(Guid managerId, string filter)
        {
            if (filter.ToLower().Equals("building"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequestsWithBuilding().Where(r => r.ManagerId == managerId);
                return GenerateReport(requests, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.ManagerId == managerId);
                List<(string, int, int, int, double)> reportWithIds = GenerateReport(requests, new EmployeeRequestReport()).ToList();
                List<(string, int, int, int, double)> reportWithNames = new List<(string, int, int, int, double)>();
                
                for (int i=0; i<reportWithIds.Count; i++)
                {
                    User employee = _userRepository.GetUserById(Guid.Parse(reportWithIds[i].Item1));
                    reportWithNames.Add((employee.Name + " " + employee.Surname, reportWithIds[i].Item2, reportWithIds[i].Item3, reportWithIds[i].Item4, reportWithIds[i].Item5));
                }

                return reportWithNames;
            }

            throw new ReportException("Invalid filter");
        }

        private static IEnumerable<(string, int, int, int, double)> GenerateReport(IEnumerable<Request> requests, RequestReport requestReport)
        {

            return requestReport.GenerateReport(requests);
        }
    }
}
