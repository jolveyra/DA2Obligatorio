using Domain;

namespace LogicInterfaces
{
    public interface IReportLogic
    {
        public IEnumerable<Report> GetReport(Guid managerId, string filter);
    }
}
