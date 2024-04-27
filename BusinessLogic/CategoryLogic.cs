using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class CategoryLogic : ICategoryLogic
    {
        private ICategoryRepository _iCategoryRepository;

        public CategoryLogic(ICategoryRepository iCategoryRepository)
        {
            _iCategoryRepository = iCategoryRepository;
        }

        public Category CreateCategory(Category category)
        {
            return _iCategoryRepository.CreateCategory(category);
        }

        public IEnumerable<Category> GetAllCategories()
        {
            throw new NotImplementedException();
        }
    }
}
