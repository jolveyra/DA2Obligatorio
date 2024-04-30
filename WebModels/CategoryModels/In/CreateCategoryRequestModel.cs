using Domain;

namespace WebModels.CategoryModels
{
    public class CreateCategoryRequestModel
    {
        public string Name { get; set; }


        public Category ToEntity()
        {
            return new Category { Name = Name };
        }
    }
}
