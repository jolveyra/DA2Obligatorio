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
    }
}
