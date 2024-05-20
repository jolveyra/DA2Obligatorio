using Domain;
using WebModels.CategoryModels;
namespace WebModels.CategoryModels
{
    public class CategoryResponseModel
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Name { get; set; }

        public CategoryResponseModel(Category category)
        {
            Id = category.Id;
            Name = category.Name;
        }

        public override bool Equals(object? obj)
        {
            return obj is CategoryResponseModel categoryResponseModel && Id == categoryResponseModel.Id;
        }
    }
}