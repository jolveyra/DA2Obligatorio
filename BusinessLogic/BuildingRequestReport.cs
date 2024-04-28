using Domain;

namespace BusinessLogic
{
    internal class BuildingRequestReport : RequestReport
    {
        protected override void FilterRequests(List<Request> requests)
        {
            requestsFilter = new List<string>();
            requestsPerFilter = new List<List<Request>>();

            foreach (Request request in requests)
            {
                if (!requestsFilter.Contains(request.Flat.Building.Name))
                {
                    requestsFilter.Add(request.Flat.Building.Name);
                    requestsPerFilter.Add(new List<Request>());
                }

                requestsPerFilter[requestsFilter.IndexOf(request.Flat.Building.Name)].Add(request);
            }
        }
    }
}
