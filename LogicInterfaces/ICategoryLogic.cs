using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace LogicInterfaces
{
    public interface ICategoryLogic
    {
        Category CreateCategory(Category category);
        public IEnumerable<Category> GetAllCategories();
    }
}
