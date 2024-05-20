using RepositoryInterfaces;
using Domain;
using DataAccess.Context;

namespace DataAccess.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {

        private readonly BuildingBossContext _context;

        public CategoryRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public Category CreateCategory(Category category)
        {
            _context.Categories.Add(category);
            _context.SaveChanges();
            return category;
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _context.Categories;
        }
    }
}
