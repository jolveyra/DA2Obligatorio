using Domain;

namespace WebModels.RequestsModels
{
    public class RequestResponseModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid FlatId { get; set; }
        public string CategoryName { get; set; }
        public Guid AssignedEmployeeId { get; set; }

        public RequestResponseModel(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            FlatId = request.Flat.Id;
            CategoryName = request.Category.Name;
            AssignedEmployeeId = request.AssignedEmployee.Id;
        }

        public override bool Equals(object obj)
        {
            return obj is RequestResponseModel r && Id == r.Id;
        }
    }
}
