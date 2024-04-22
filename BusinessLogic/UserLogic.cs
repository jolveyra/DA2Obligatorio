using System.Text.RegularExpressions;
using CustomExceptions.BusinessLogic;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class UserLogic : IAdministratorLogic, IMaintenanceEmployeeLogic, IAuthorizationLogic
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
            user.Role = Role.Administrator;
            ValidateUser(user);

            List<User> users = _userRepository.GetAllUsers().ToList();

            if (users.Exists(u => u.Email == user.Email))
            {
                throw new UserException("A user with the same email already exists");
            }

            return _userRepository.CreateUser(user);
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

        public void ValidateUser(User user)
        {
            if (!user.Email.Contains("@") || !user.Email.Contains(".") || user.Email.Length < 5)
            {
                throw new UserException("An Email must contain '@', '.' and be longer than 4 characters long");
            }

            if (!isValidPassword(user.Password))
            {
                throw new UserException("A Password must an uppercase and lowercase letter, a number and must be longer than 7 characters long");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new UserException("The Name field cannot be empty");
            }

            if (string.IsNullOrEmpty(user.Surname))
            {
                throw new UserException("The Surname field cannot be empty");
            }
        }

        private static bool isValidPassword(string password)
        {
            return Regex.IsMatch(password, "[A-Z]") && 
                   Regex.IsMatch(password, "[a-z]") &&
                   Regex.IsMatch(password, "[0-9]") &&
                   password.Length >= 8;
        }
    }
}
