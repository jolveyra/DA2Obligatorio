using Domain;

namespace WebModels.RequestsModels
{
    public class RequestCreateModel
    {
        public string Description { get; set; }
        public Guid BuildingId { get; set; }
        public Guid flatId { get; set; }
        public string CategoryName { get; set; }

        public Request ToEntity()
        {
            if (String.IsNullOrEmpty(Description) || BuildingId == null || flatId == null || String.IsNullOrEmpty(CategoryName))
            {
                throw new MissingFieldException("One of the parameters of the body is missing");
            }

            return new Request
            {
                Description = Description,
                BuildingId = BuildingId,
                FlatId = flatId,
                Category = new Category { Name = CategoryName }
            };
        }
    }
}
