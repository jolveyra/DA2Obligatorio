using Domain;

namespace WebModels.RequestsModels
{
    public class RequestCreateModel
    {
        public string Description { get; set; }
        public Guid FlatId { get; set; }
        public Guid BuildingId { get; set; }
        public string CategoryName { get; set; }
        public Guid AssignedEmployee { get; set; }

        public Request ToEntity()
        {
            return new Request
            {
                Description = Description,
                Flat = new Flat() { Id = FlatId },
                BuildingId = BuildingId,
                Category = new Category { Name = CategoryName },
                AssignedEmployeeId = AssignedEmployee
            };
        }
    }
}
