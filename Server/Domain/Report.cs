namespace Domain
{
    public class Report
    {
        public string Filter { get; set; }
        public int Pending { get; set; }
        public int InProgress { get; set; }
        public int Completed { get; set; }
        public double AvgTimeToComplete { get; set; }

        public Report(string filter)
        {
            Filter = filter;
            Pending = 0;
            InProgress = 0;
            Completed = 0;
            AvgTimeToComplete = 0;
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
