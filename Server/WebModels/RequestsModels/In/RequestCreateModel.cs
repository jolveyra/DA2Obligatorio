using Domain;

namespace WebModels.RequestsModels
{
    public class RequestCreateModel
    {
        public string Description { get; set; }
        public Guid FlatId { get; set; }
        public string CategoryName { get; set; }
        public Guid AssignedEmployeeId { get; set; }

        public Request ToEntity()
        {
            return new Request
            {
                Description = Description,
                Flat = new Flat() { Id = FlatId },
                Category = new Category { Name = CategoryName },
                AssignedEmployee = new User() { Id = AssignedEmployeeId }
            };
        }
    }
}
