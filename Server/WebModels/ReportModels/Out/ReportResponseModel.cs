using Domain;

namespace WebModels.ReportModels
{
    public class ReportResponseModel
    {
        public string FilterName { get; set; }
        public int PendingRequests { get; set; }
        public int InProgressRequests { get; set; }
        public int CompletedRequests { get; set; }
        public double AverageCompletionTime { get; set; }

        public ReportResponseModel(Report report)
        {
            FilterName = report.Filter;
            PendingRequests = report.Pending;
            InProgressRequests = report.InProgress;
            CompletedRequests = report.Completed;
            AverageCompletionTime = report.AvgTimeToComplete;
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
