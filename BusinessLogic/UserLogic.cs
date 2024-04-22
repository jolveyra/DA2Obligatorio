using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class UserLogic : IAdministratorLogic, IMaintenanceEmployeeLogic
    {
        private readonly ITokenRepository _tokenRepository;
        private readonly IUserRepository _userRepository;

        public UserLogic(IUserRepository userRepository, ITokenRepository tokenRepository) 
        { 
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
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

        public string GetUserRoleByToken(Guid token)
        {
            return _userRepository.GetUserById(_tokenRepository.GetUserIdByToken(token)).Role.ToString();
        }
        public IEnumerable<User> GetAllMaintenanceEmployees()
        {
            return _userRepository.GetAllUsers().Where(u => u.Role == Role.MaintenanceEmployee);
        }
    }
}
