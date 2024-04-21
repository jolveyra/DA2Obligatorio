using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class UserLogic : IAdministratorLogic, IMaintenanceEmployeeLogic
    {
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public User CreateAdministrator(User user)
        {
            throw new NotImplementedException();
        }

        public User CreateMaintenanceEmployee(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllAdministrators()
        {
            return _userRepository.GetAllUsers().Where(u => u.Role == Role.Administrator);
        }

        public IEnumerable<User> GetAllMaintenanceEmployees()
        {
            throw new NotImplementedException();
        }
    }
}
