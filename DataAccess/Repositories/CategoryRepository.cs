using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RepositoryInterfaces;
using Domain;
using DataAccess.Context;

namespace DataAccess
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
