using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BuildingBossContext _context;

        public UserRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public User CreateUser(User user)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public User GetUserById(Guid id)
        {
            User? user = _context.Users.FirstOrDefault(u => u.Id == id);

            if (user == null)
            {
                throw new ArgumentException("User not found");
            }

            return user;
        }
    }
}
