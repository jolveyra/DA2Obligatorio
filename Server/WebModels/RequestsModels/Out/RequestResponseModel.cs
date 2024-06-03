using Domain;
using WebModels.BuildingModels;
using WebModels.UserModels;

namespace WebModels.RequestsModels
{
    public class RequestResponseModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public FlatResponseModel Flat { get; set; }
        public BuildingWithoutFlatsResponseModel Building { get; set; }
        public string CategoryName { get; set; }
        public UserResponseModel AssignedEmployee { get; set; }
        public string Status { get; set; }

        public RequestResponseModel(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            Flat = new FlatResponseModel(request.Flat);
            Building = new BuildingWithoutFlatsResponseModel(request.Building);
            CategoryName = request.Category.Name;
            Status = request.Status.ToString();
            AssignedEmployee = new UserResponseModel(request.AssignedEmployee);
        }

        public override bool Equals(object obj)
        {
            return obj is RequestResponseModel r && Id == r.Id;
        }
    }
}
