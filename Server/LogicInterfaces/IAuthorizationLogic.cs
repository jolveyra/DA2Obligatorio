using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Domain;
using System.Threading.Tasks;

namespace LogicInterfaces
{
    public interface IAuthorizationLogic
    {
        public Guid GetUserIdByToken(Guid token);
        public string GetUserRoleByToken(Guid guid);
    }
}
