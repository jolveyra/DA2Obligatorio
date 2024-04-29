using Domain;

namespace BusinessLogic
{
    internal abstract class RequestReport
    {
        protected List<string> requestsFilter;
        protected List<List<Request>> requestsPerFilter;

        public IEnumerable<(string, int, int, int, double)> GenerateReport(List<Request> requests)
        {
            FilterRequests(requests);
            List<int> pendingRequests = GatherPendingRequests();
            List<int> inProgressRequests = GatherInProgressRequests();
            List<(int, double)> completedAndAvgTimeRequests = GatherCompletedAndAvgTimeRequests();
            return BringReportTogether(pendingRequests, inProgressRequests, completedAndAvgTimeRequests);
        }

        protected abstract void FilterRequests(List<Request> requests);

        private List<int> GatherPendingRequests()
        {
            List<int> pendingRequests = new List<int>();

            foreach (List<Request> requestList in requestsPerFilter)
            {
                int pending = 0;

                foreach (Request request in requestList)
                {
                    if (request.Status == RequestStatus.Pending)
                    {
                        pending++;
                    }
                }

                pendingRequests.Add(pending);
            }

            return pendingRequests;
        }

        private List<int> GatherInProgressRequests()
        {
            List<int> inProgressRequests = new List<int>();

            foreach (List<Request> requestList in requestsPerFilter)
            {
                int inProgress = 0;

                foreach (Request request in requestList)
                {
                    if (request.Status == RequestStatus.InProgress)
                    {
                        inProgress++;
                    }
                }

                inProgressRequests.Add(inProgress);
            }

            return inProgressRequests;
        }

        private List<(int, double)> GatherCompletedAndAvgTimeRequests()
        {
            List<(int, double)> completedRequests = new List<(int, double)>();
            
            foreach (List<Request> requestList in requestsPerFilter)
            {
                double avgTimeToComplete = 0;
                int completed = 0;

                foreach (Request request in requestList)
                {
                    if (request.Status == RequestStatus.Completed)
                    {
                        avgTimeToComplete += (request.CompletionDate - request.StartingDate).Hours;
                        completed++;
                    }
                }

                if (completed > 0)
                {
                    avgTimeToComplete /= completed;
                }

                completedRequests.Add((completed, avgTimeToComplete));
            }

            return completedRequests;
        }

        private IEnumerable<(string, int, int, int, double)> BringReportTogether(List<int> pendingRequests, List<int> inProgressRequests, List<(int, double)> completedAndAvgTimeRequests)
        {
            List<(string, int, int, int, double)> report = new List<(string, int, int, int, double)>();

            for (int i = 0; i < requestsFilter.Count; i++)
            {
                report.Add((requestsFilter[i], pendingRequests[i], inProgressRequests[i], completedAndAvgTimeRequests[i].Item1, completedAndAvgTimeRequests[i].Item2));
            }

            return report;
        }
    }
}
