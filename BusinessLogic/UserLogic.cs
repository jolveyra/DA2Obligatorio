using Domain;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class UserLogic
    {
        private IUserRepository _userRepository;
        private ITokenRepository _tokenRepository;

        public UserLogic(IUserRepository userRepository, ITokenRepository tokenRepository) 
        { 
            _userRepository = userRepository;
            _tokenRepository = tokenRepository;
        }

        public string GetUserRoleByToken(Guid token)
        {
            Guid userId = _tokenRepository.GetUserIdByToken(token);
            return _userRepository.GetUserById(userId).Role.ToString();
        }
    }
}
