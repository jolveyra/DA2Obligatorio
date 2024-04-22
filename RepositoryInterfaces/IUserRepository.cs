using Domain;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        User? GetUserByEmail(string email);
        User GetUserById(Guid id);
    }
}
