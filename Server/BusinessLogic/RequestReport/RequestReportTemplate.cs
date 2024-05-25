using Domain;

namespace BusinessLogic
{
    internal abstract class RequestReportTemplate
    {
        protected List<Report> requestsReports;
        protected List<List<Request>> requestsPerFilter;

        public IEnumerable<Report> GenerateReport(IEnumerable<Request> requests)
        {
            FilterRequests(requests);
            GatherRequestsAmountByState();
            return requestsReports;
        }

        protected abstract void FilterRequests(IEnumerable<Request> requests);

        private void GatherRequestsAmountByState()
        {
            for (int i = 0; i < requestsPerFilter.Count; i++)
            {
                IterateThroughListOfRequestByFilter(i);
            }
        }

        private void IterateThroughListOfRequestByFilter(int indexOfReportFilter)
        {
            Report report = requestsReports[indexOfReportFilter];

            for (int j = 0; j < requestsPerFilter[indexOfReportFilter].Count; j++)
            {
                IterateThroughRequests(report, requestsPerFilter[indexOfReportFilter][j]);
            }
                
            if (report.Completed > 0)
            {
                report.AvgTimeToComplete /= report.Completed;
            }

        }

        private void IterateThroughRequests(Report report, Request request)
        {
            if (request.Status == RequestStatus.Pending)
            {
                report.Pending++;
            }
                    
            if (request.Status == RequestStatus.InProgress)
            {
                report.InProgress++;
            }

            if (request.Status == RequestStatus.Completed)
            {
                report.Completed++;
                report.AvgTimeToComplete += (request.CompletionDate - request.StartingDate).Hours;
            }
        }
    }
}
