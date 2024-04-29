namespace LogicInterfaces
{
    public interface IReportLogic
    {
        public IEnumerable<(string, int, int, int, double)> GetReport(Guid managerId, string filter);
    }
}
