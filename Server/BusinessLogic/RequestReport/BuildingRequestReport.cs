using Domain;

namespace BusinessLogic
{
    internal class BuildingRequestReport : RequestReportTemplate
    {
        protected override void FilterRequests(IEnumerable<Request> requests)
        {
            requestsReports = new List<RequestReport>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                RequestReport? report = requestsReports.Find(report => report.Filter == request.Flat.Building.Name);

                if (report is null)
                {
                    report = new RequestReport(request.Flat.Building.Name);
                    requestsReports.Add(report);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsReports.IndexOf(report)].Add(request);
            }
        }
    }
}
