using Domain;

namespace WebModels.RequestsModels
{
    public class RequestUpdateModel
    {
        public string Description { get; set; }
        public string CategoryName { get; set; }
        public Guid AssignedEmployeeId { get; set; }

        public Request ToEntity(Guid id)
        {
            if (String.IsNullOrEmpty(Description) || String.IsNullOrEmpty(CategoryName) || AssignedEmployeeId == null)
            { 
                throw new MissingFieldException("One of the parameters of the body is missing");
            }

            return new Request
            {
                Id = id,
                Description = Description,
                Category = new Category { Name = CategoryName },
                AssignedEmployeeId = AssignedEmployeeId
            };
        }
    }
}
