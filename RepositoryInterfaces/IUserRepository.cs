using Domain;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
    }
}
