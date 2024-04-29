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

        public Session CreateSession(Session session)
        {
            _context.Sessions.Add(session);
            _context.SaveChanges();

            return session;
        }

        public Session GetSessionByToken(Guid guid)
        {
            Session? session = _context.Sessions.FirstOrDefault(s => s.Id == guid);

            if (session == null)
            {
                throw new ArgumentException("Session not found");
            }

            return session;
        }

        public Session GetSessionByUserId(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
