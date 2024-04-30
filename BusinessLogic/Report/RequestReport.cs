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
            List<(int, int, int, double)> requestTuples = GatherPendingRequests();
            //List<int> inProgressRequests = GatherInProgressRequests();
            //List<(int, double)> completedAndAvgTimeRequests = GatherCompletedAndAvgTimeRequests();
            return BringReportTogether(requestTuples);
        }

        protected abstract void FilterRequests(IEnumerable<Request> requests);

        private List<(int, int, int, double)> GatherPendingRequests()
        {
            List<(int, int, int, double)> requestTuples = new List<(int, int, int, double)>();

            foreach (List<Request> requestList in requestsPerFilter)
            {
                int pending = 0;
                int inProgress = 0;
                int completed = 0;
                double avgTimeToComplete = 0;

                foreach (Request request in requestList)
                {
                    if (request.Status == RequestStatus.Pending)
                    {
                        pending++;
                    }
                    
                    if (request.Status == RequestStatus.InProgress)
                    {
                        inProgress++;
                    }
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

                requestTuples.Add((pending, inProgress, completed, avgTimeToComplete));
            }

            return requestTuples;
        }

        //private List<int> GatherInProgressRequests()
        //{
        //    List<int> inProgressRequests = new List<int>();

        //    foreach (List<Request> requestList in requestsPerFilter)
        //    {
        //        int inProgress = 0;

        //        foreach (Request request in requestList)
        //        {
        //            if (request.Status == RequestStatus.InProgress)
        //            {
        //                inProgress++;
        //            }
        //        }

        //        inProgressRequests.Add(inProgress);
        //    }

        //    return inProgressRequests;
        //}

        //private List<(int, double)> GatherCompletedAndAvgTimeRequests()
        //{
        //    List<(int, double)> completedRequests = new List<(int, double)>();
            
        //    foreach (List<Request> requestList in requestsPerFilter)
        //    {
        //        double avgTimeToComplete = 0;
        //        int completed = 0;

        //        foreach (Request request in requestList)
        //        {
        //            if (request.Status == RequestStatus.Completed)
        //            {
        //                avgTimeToComplete += (request.CompletionDate - request.StartingDate).Hours;
        //                completed++;
        //            }
        //        }

        //        if (completed > 0)
        //        {
        //            avgTimeToComplete /= completed;
        //        }

        //        completedRequests.Add((completed, avgTimeToComplete));
        //    }

        //    return completedRequests;
        //}

        private IEnumerable<(string, int, int, int, double)> BringReportTogether(List<(int, int, int, double)> requestTuples)
        {
            List<(string, int, int, int, double)> report = new List<(string, int, int, int, double)>();

            for (int i = 0; i < requestsFilter.Count; i++)
            {
                report.Add((requestsFilter[i], requestTuples[i].Item1, requestTuples[i].Item2, requestTuples[i].Item3, requestTuples[i].Item4));
            }

            return report;
        }
    }
}
