using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace RepositoryInterfaces
{
    public interface ICategoryRepository
    {
        public List<Category> GetAllCategories();
        public Category CreateCategory(Category category);
    }
}
