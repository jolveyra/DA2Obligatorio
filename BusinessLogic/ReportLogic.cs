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
            User manager = _userRepository.GetUserById(managerId);

            if (filter.ToLower().Equals("building"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.Flat.Building.Manager.Equals(manager));
                return GenerateReport(requests, new BuildingRequestReport());
            }
            if (filter.ToLower().Equals("employee"))
            {
                IEnumerable<Request> requests = _requestRepository.GetAllRequests().Where(r => r.Flat.Building.Manager.Equals(manager));
                var report =  GenerateReport(requests, new EmployeeRequestReport()).ToList();

                for (int i = 0; i < report.Count; i++)
                {
                    User employee = _userRepository.GetUserById(Guid.Parse(report[i].Item1));
                    report[i] = (employee.Name + " " + employee.Surname, report[i].Item2, report[i].Item3, report[i].Item4, report[i].Item5);
                }

                return report;
            }

            throw new ReportException("Invalid filter");
        }

        private static IEnumerable<(string, int, int, int, double)> GenerateReport(IEnumerable<Request> requests, RequestReport requestReport)
        {

            return requestReport.GenerateReport(requests);
        }
    }
}
