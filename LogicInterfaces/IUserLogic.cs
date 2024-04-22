using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IUserLogic
    {
        public string GetUserRoleByToken(Guid guid);
    }
}
