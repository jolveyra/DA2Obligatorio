namespace LogicInterfaces
{
    public interface IReportLogic
    {
        public List<(string, int, int, int, double)> GetReport(string filter);
    }
}
