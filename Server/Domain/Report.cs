namespace Domain
{
    public class Report
    {
        public string Filter { get; set; }
        public int Pending { get; set; } = 0;
        public int InProgress { get; set; } = 0;
        public int Completed { get; set; } = 0;
        public double AvgTimeToComplete { get; set; } = 0;

        public Report(string filter)
        {
            Filter = filter;
        }

        public override bool Equals(object obj)
        {
            return obj is Report report && 
                   Filter == report.Filter && 
                   Pending == report.Pending && 
                   InProgress == report.InProgress && 
                   Completed == report.Completed && 
                   AvgTimeToComplete == report.AvgTimeToComplete;
        }
    }
}
