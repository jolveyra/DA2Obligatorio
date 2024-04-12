using Domain;

namespace WebModels.RequestsModels
{
    public class RequestResponseModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public string Category { get; set; }

        public RequestResponseModel(Request request)
        {
            Id = request.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is RequestResponseModel r && Id == r.Id;
        }
    }
}
