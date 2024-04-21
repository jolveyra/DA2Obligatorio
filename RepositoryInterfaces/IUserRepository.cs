using Domain;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUserById(Guid id);
    }
}
