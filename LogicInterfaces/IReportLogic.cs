namespace LogicInterfaces
{
    public interface IReportLogic
    {
        public IEnumerable<(string, int, int, int, double)> GetReport(string filter);
    }
}
