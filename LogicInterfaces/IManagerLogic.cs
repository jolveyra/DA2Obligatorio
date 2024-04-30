using Domain;

namespace LogicInterfaces
{
    public interface IManagerLogic
    {
        public IEnumerable<User> GetAllManagers();
    }
}
