using Domain;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUserById(Guid id);
        IEnumerable<User> GetAllUsers();
        User GetUserByEmail(string email);
        User CreateUser(User user);
    }
}
