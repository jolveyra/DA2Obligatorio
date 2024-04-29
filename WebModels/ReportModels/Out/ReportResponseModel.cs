namespace WebModels.ReportModels.Out
{
    public class ReportResponseModel
    {
        public string FilterName { get; set; }
        public int PendingRequests { get; set; }
        public int InProgressRequests { get; set; }
        public int CompletedRequests { get; set; }
        public double AverageCompletionTime { get; set; }

        public ReportResponseModel((string, int, int, int, double) tuple)
        {
            FilterName = tuple.Item1;
            PendingRequests = tuple.Item2;
            InProgressRequests = tuple.Item3;
            CompletedRequests = tuple.Item4;
            AverageCompletionTime = tuple.Item5;
        }

        public override bool Equals(object? obj)
        {
            return obj is ReportResponseModel r && 
                   r.FilterName.Equals(FilterName) && 
                   r.PendingRequests == PendingRequests && 
                   r.InProgressRequests == InProgressRequests && 
                   r.CompletedRequests == CompletedRequests && 
                   r.AverageCompletionTime == AverageCompletionTime;
        }
    }
}
