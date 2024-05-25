using Domain;

namespace BusinessLogic
{
    internal class EmployeeRequestReport : RequestReportTemplate
    {
        protected override void FilterRequests(IEnumerable<Request> requests)
        {
            requestsReports = new List<RequestReport>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                string employeeName = request.AssignedEmployeeId.ToString();
                RequestReport? report = requestsReports.Find(report => report.Filter == employeeName);

                if (report is null)
                {
                    report = new RequestReport(employeeName);
                    requestsReports.Add(report);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsReports.IndexOf(report)].Add(request);
            }
        }
    }
}