using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class SessionLogic : IAuthorizationLogic
    {
        private readonly ISessionRepository _sessionRepository;
        private readonly IUserRepository _userRepository;

        public SessionLogic(ISessionRepository sessionRepository, IUserRepository userRepository)
        {
            _sessionRepository = sessionRepository;
            _userRepository = userRepository;
        }

        public Guid GetUserIdByToken(Guid token)
        {
            return _sessionRepository.GetSessionByToken(token).UserId;
        }

        public string GetUserRoleByToken(Guid token)
        {
            return _userRepository.GetUserById(_sessionRepository.GetSessionByToken(token).UserId).Role.ToString();
        }
    }
}
