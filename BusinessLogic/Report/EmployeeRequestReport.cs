using Domain;

namespace BusinessLogic
{
    internal class EmployeeRequestReport : RequestReport
    {
        protected override void FilterRequests(List<Request> requests)
        {
            requestsFilter = new List<string>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                string employeeName = request.AssignedEmployee.Name + " " + request.AssignedEmployee.Surname;
                if (!requestsFilter.Contains(employeeName))
                {
                    requestsFilter.Add(employeeName);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsFilter.IndexOf(employeeName)].Add(request);
            }
        }
    }
}