using Domain;

namespace BusinessLogic
{
    internal abstract class RequestReport
    {
        protected List<string> requestsFilter;
        protected List<List<Request>> requestsPerFilter;

        public IEnumerable<(string, int, int, int, double)> GenerateReport(IEnumerable<Request> requests)
        {
            FilterRequests(requests);
            List<(int, int, int, double)> requestTuples = GatherRequestsAmountByState();
            return BringReportTogether(requestTuples);
        }

        protected abstract void FilterRequests(IEnumerable<Request> requests);

        private List<(int, int, int, double)> GatherRequestsAmountByState()
        {
            List<(int, int, int, double)> requestTuples = new List<(int, int, int, double)>();

            foreach (List<Request> requestList in requestsPerFilter)
            {
                IterateThroughListOfRequestByFilter(requestList, ref requestTuples);
            }

            return requestTuples;
        }

        private static void IterateThroughListOfRequestByFilter(List<Request> requestList, ref List<(int, int, int, double)> requestTuples)
        {
            int pending = 0;
            int inProgress = 0;
            int completed = 0;
            double avgTimeToComplete = 0;

            foreach (Request request in requestList)
            {
                pending = IterateThroughRequests(request, ref pending, ref inProgress, ref avgTimeToComplete, ref completed);
            }
                
            if (completed > 0)
            {
                avgTimeToComplete = CalculateAverageTimeOfCompletion(avgTimeToComplete, completed);
            }

            requestTuples.Add((pending, inProgress, completed, avgTimeToComplete));
        }

        private static double CalculateAverageTimeOfCompletion(double avgTimeToComplete, int completed)
        {
            avgTimeToComplete /= completed;
            return avgTimeToComplete;
        }

        private static int IterateThroughRequests(Request request, ref int pending, ref int inProgress, ref double avgTimeToComplete,
            ref int completed)
        {
            if (request.Status == RequestStatus.Pending)
            {
                IncreasePendingRequestCount(ref pending);
            }
                    
            if (request.Status == RequestStatus.InProgress)
            {
                IncreaseInProgressRequestCount(ref inProgress);
            }

            if (request.Status == RequestStatus.Completed)
            {
                IncreaseCompletedRequestsCount(ref completed);
                IncreaseSumOfTimeToComplete(request, ref avgTimeToComplete);
            }

            return pending;
        }

        private static void IncreaseSumOfTimeToComplete(Request request, ref double avgTimeToComplete)
        {
            avgTimeToComplete += (request.CompletionDate - request.StartingDate).Hours;
        }

        private static void IncreaseCompletedRequestsCount(ref int completed)
        {
            completed++;
        }

        private static void IncreaseInProgressRequestCount(ref int inProgress)
        {
            inProgress++;
        }

        private static void IncreasePendingRequestCount(ref int pending)
        {
            pending++;
        }

        private IEnumerable<(string, int, int, int, double)> BringReportTogether(List<(int, int, int, double)> requestTuples)
        {
            List<(string, int, int, int, double)> report = new List<(string, int, int, int, double)>();

            for (int i = 0; i < requestsFilter.Count; i++)
            {
                CreateReportTupleWithFilter(requestTuples, report, i);
            }

            return report;
        }

        private void CreateReportTupleWithFilter(List<(int, int, int, double)> requestTuples, List<(string, int, int, int, double)> report, int i)
        {
            report.Add((requestsFilter[i], requestTuples[i].Item1, requestTuples[i].Item2, requestTuples[i].Item3, requestTuples[i].Item4));
        }
    }
}
