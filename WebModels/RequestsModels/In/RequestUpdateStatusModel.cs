using Domain;

namespace WebModels.RequestsModels
{
    public class RequestUpdateStatusModel
    {
        public string Status { get; set; }

        public RequestStatus ToEntity()
        {
            return (RequestStatus)Enum.Parse(typeof(RequestStatus), Status);
        }
    }
}
