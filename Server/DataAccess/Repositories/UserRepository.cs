using DataAccess.Context;
using Domain;
using RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class UserRepository : IUserRepository, IConstructorCompanyAdministratorRepository
    {
        private readonly BuildingBossContext _context;

        public UserRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public ConstructorCompanyAdministrator CreateConstructorCompanyAdministrator(ConstructorCompanyAdministrator administrator)
        {
            _context.Users.Add(administrator);
            _context.SaveChanges();

            return administrator;
        }

        public User CreateUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();

            return user;
        }

        public IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators()
        {
            return _context.Users.OfType<ConstructorCompanyAdministrator>();
        }

        public IEnumerable<User> GetAllUsers()
        {
            return _context.Users;
        }

        public ConstructorCompanyAdministrator GetConstructorCompanyAdministratorByUserId(Guid userId)
        {
            ConstructorCompanyAdministrator? administrator = _context.Users
                .OfType<ConstructorCompanyAdministrator>()
                .FirstOrDefault(u => u.Id == userId);

            if (administrator == null)
            {
                throw new ArgumentException("Constructor company administrator not found");
            }

            return administrator;
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

        public ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator)
        {
            _context.Users.Update(constructorCompanyAdministrator);
            _context.SaveChanges();

            return constructorCompanyAdministrator;
        }

        public User UpdateUser(User user)
        {
            _context.Users.Update(user);
            _context.SaveChanges();

            return user;
        }
    }
}
