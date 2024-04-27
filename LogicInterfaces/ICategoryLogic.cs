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
        public IEnumerable<Category> GetAllCategories();
    }
}
