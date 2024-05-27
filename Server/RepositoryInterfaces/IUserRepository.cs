using Domain;

namespace RepositoryInterfaces
{
    public interface IUserRepository
    {
        User GetUserById(Guid id);
        IEnumerable<User> GetAllUsers();
        User CreateUser(User user);
        User UpdateUser(User user);
        ConstructorCompanyAdministrator GetConstructorCompanyAdministratorByUserId(Guid userId);
    }
}
