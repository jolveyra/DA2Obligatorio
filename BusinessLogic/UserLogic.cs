using System.Text.RegularExpressions;
using CustomExceptions.BusinessLogic;
using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class UserLogic : IAdministratorLogic, IMaintenanceEmployeeLogic, IAuthorizationLogic, IUserSettingsLogic, ILoginLogic
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        private const int minPasswordLength = 6;

        public UserLogic(IUserRepository userRepository, ISessionRepository sessionRepository)
        {
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
        }

        public User CreateAdministrator(User user)
        {
            user.Role = Role.Administrator;
            return CreateUser(_userRepository, _sessionRepository, user);
        }

        public User CreateMaintenanceEmployee(User user)
        {
            user.Role = Role.MaintenanceEmployee;
            return CreateUser(_userRepository, _sessionRepository, user);
        }

        public static User CreateManager(IUserRepository userRepository, ISessionRepository sessionRepository, User user)
        {
            user.Role = Role.Manager;
            return CreateUser(userRepository, sessionRepository, user);
        }

        private static User CreateUser(IUserRepository userRepository, ISessionRepository sessionRepository,User user)
        {
            ValidateUser(user);

            if (ExistsUserEmail(userRepository, user.Email))
            {
                throw new UserException("A user with the same email already exists");
            }

            User newUser = userRepository.CreateUser(user);
            sessionRepository.CreateSession(new Session() { UserId = newUser.Id });

            return newUser;
        }

        public IEnumerable<User> GetAllAdministrators()
        {
            return _userRepository.GetAllUsers().Where(u => u.Role == Role.Administrator);
        }

        public string GetUserRoleByToken(Guid token)
        {
            return _userRepository.GetUserById(_sessionRepository.GetSessionByToken(token).UserId).Role.ToString();
        }

        public IEnumerable<User> GetAllMaintenanceEmployees()
        {
            return _userRepository.GetAllUsers().Where(u => u.Role == Role.MaintenanceEmployee);
        }

        public static void ValidateUser(User user)
        {
            if (!isValidEmail(user.Email))
            {
                throw new UserException("An Email must contain '@', '.' and be longer than 4 characters long");
            }

            if (!isValidPassword(user.Password))
            {
                throw new UserException("A Password must an uppercase and lowercase letter, a number and must be longer than 5 characters long");
            }

            if (string.IsNullOrEmpty(user.Name))
            {
                throw new UserException("The Name field cannot be empty");
            }

            if (string.IsNullOrEmpty(user.Surname) && user.Role != Role.Manager)
            {
                throw new UserException("The Surname field cannot be empty");
            }
        }

        private static bool isValidPassword(string password)
        {
            return Regex.IsMatch(password, "[A-Z]") && 
                   Regex.IsMatch(password, "[a-z]") &&
                   Regex.IsMatch(password, "[0-9]") &&
                   password.Length >= minPasswordLength;
        }

        public static bool isValidEmail(string email)
        {
            return email.Contains("@") && email.Contains(".") && email.Length > 5;
        }

        public static bool ExistsUserEmail(IUserRepository userRepository, string email)
        {
            return userRepository.GetAllUsers().Any(u => u.Email.ToLower().Equals(email.ToLower()));
        }

        public User GetUserById(Guid userId)
        {
            return _userRepository.GetUserById(userId);
        }

        public User UpdateUserSettings(Guid userId, User user)
        {
            User userToUpdate = _userRepository.GetUserById(userId);

            userToUpdate.Name = user.Name;
            userToUpdate.Surname = user.Surname;
            userToUpdate.Password = user.Password;

            ValidateUser(userToUpdate);

            return _userRepository.UpdateUser(userToUpdate);
        }

        public Guid GetUserIdByToken(Guid token)
        {
            return _sessionRepository.GetSessionByToken(token).UserId;
        }

        public Guid Login(User user)
        {
            User? existingUser = _userRepository.GetAllUsers().FirstOrDefault(u => u.Email == user.Email && u.Password == user.Password);

            if (existingUser == null)
            {
                throw new UserException("Invalid email or password");
            }

            Session session = _sessionRepository.GetSessionByUserId(existingUser.Id);

            return session.Id;
        }
    }
}
