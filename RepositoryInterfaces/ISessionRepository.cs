
namespace RepositoryInterfaces
{
    public interface ISessionRepository
    {
        public Guid GetUserIdByToken(Guid guid);
    }
}
