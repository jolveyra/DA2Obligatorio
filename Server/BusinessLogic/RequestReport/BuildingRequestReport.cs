using Domain;

namespace BusinessLogic
{
    internal class BuildingRequestReport : RequestReportTemplate
    {
        protected override void FilterRequests(IEnumerable<Request> requests)
        {
            requestsReports = new List<Report>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                Report? report = requestsReports.Find(report => report.Filter == request.Flat.Building.Name);

                if (report is null)
                {
                    report = new Report(request.Flat.Building.Name);
                    requestsReports.Add(report);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsReports.IndexOf(report)].Add(request);
            }
        }
    }
}
