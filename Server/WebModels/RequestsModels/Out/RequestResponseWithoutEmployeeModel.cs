using Domain;
using WebModels.BuildingModels;

namespace WebModels.RequestsModels
{
    public class RequestResponseWithoutEmployeeModel
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public FlatResponseModel Flat { get; set; }
        public BuildingWithoutFlatsResponseModel Building { get; set; }
        public string CategoryName { get; set; }
        public string Status { get; set; }

        public RequestResponseWithoutEmployeeModel(Request request)
        {
            Id = request.Id;
            Description = request.Description;
            Flat = new FlatResponseModel(request.Flat);
            Building = new BuildingWithoutFlatsResponseModel(request.Building);
            CategoryName = request.Category.Name;
            Status = request.Status.ToString();
        }

        public override bool Equals(object obj)
        {
            return obj is RequestResponseWithoutEmployeeModel r && Id == r.Id;
        }
    }
}
