using Domain;

namespace BusinessLogic
{
    internal class EmployeeRequestReport : RequestReport
    {
        protected override void FilterRequests(IEnumerable<Request> requests)
        {
            requestsFilter = new List<string>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                string employeeId = request.AssignedEmployeeId.ToString();
                if (!requestsFilter.Contains(employeeId))
                {
                    requestsFilter.Add(employeeId);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsFilter.IndexOf(employeeId)].Add(request);
            }
        }
    }
}