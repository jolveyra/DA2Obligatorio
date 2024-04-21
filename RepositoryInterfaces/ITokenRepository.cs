
namespace RepositoryInterfaces
{
    public interface ITokenRepository
    {
        public Guid GetUserIdByToken(Guid guid);
    }
}
