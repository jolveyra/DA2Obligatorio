
using Domain;

namespace RepositoryInterfaces
{
    public interface ISessionRepository
    {
        public Session CreateSession(Session session);
        public Session GetSessionByToken(Guid guid);
        public Session GetSessionByUserId(Guid id);
    }
}
