using Domain;

namespace WebModels.RequestsModels
{
    public class RequestCreateModel
    {
        public string Description { get; set; }
        public Guid FlatId { get; set; }
        public string CategoryName { get; set; }

        public Request ToEntity()
        {
            if (String.IsNullOrEmpty(Description) || FlatId == null || String.IsNullOrEmpty(CategoryName))
            {
                throw new ArgumentException("There is a missing field in the request's body");
            }

            return new Request
            {
                Description = Description,
                Flat = new Flat() { Id = FlatId },
                Category = new Category { Name = CategoryName }
            };
        }
    }
}
