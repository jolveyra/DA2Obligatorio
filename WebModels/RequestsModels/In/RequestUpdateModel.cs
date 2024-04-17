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
                throw new MissingFieldException("There is a missing field in the request's body");
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
