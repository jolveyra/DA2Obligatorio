using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class SessionRepository : ISessionRepository
    {
        private readonly BuildingBossContext _context;

        public SessionRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public Guid GetUserIdByToken(Guid guid)
        {
            Session? session = _context.Sessions.FirstOrDefault(s => s.Id == guid);

            if (session == null)
            {
                throw new ArgumentException("Session not found");
            }

            return session.UserId;
        }
    }
}
