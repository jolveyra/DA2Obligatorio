
using Domain;

namespace RepositoryInterfaces
{
    public interface ISessionRepository
    {
        public Session CreateSession(Session session);
        public Guid GetUserIdByToken(Guid guid);
    }
}
