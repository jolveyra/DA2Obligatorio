using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;
using CustomExceptions.BusinessLogic;

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
            CheckNotNullOrEmptyCategoryName(category);

            CheckUniqueCategoryName(category);

            return _iCategoryRepository.CreateCategory(category);
        }

        private static void CheckNotNullOrEmptyCategoryName(Category category)
        {
            if (string.IsNullOrEmpty(category.Name))
            {
                throw new CategoryException("Category name cannot be empty");
            }
        }

        private void CheckUniqueCategoryName(Category category)
        {
            if (GetAllCategories().Any(c => c.Name == category.Name))
            {
                throw new CategoryException("Category with same name already exists");
            }
        }

        public IEnumerable<Category> GetAllCategories()
        {
            return _iCategoryRepository.GetAllCategories();
        }
    }
}
