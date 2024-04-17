using Domain;

namespace WebModels.RequestsModels
{
    public class RequestResponseModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BuildingId { get; set; }
        public Guid FlatId { get; set; }
        public Category Category { get; set; }
        public Guid AssignedEmployeeId { get; set; }

        public RequestResponseModel(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            BuildingId = request.BuildingId;
            FlatId = request.FlatId;
            Category = request.Category;
            AssignedEmployeeId = request.AssignedEmployeeId;
        }

        public override bool Equals(object obj)
        {
            return obj is RequestResponseModel r && Id == r.Id;
        }
    }
}
