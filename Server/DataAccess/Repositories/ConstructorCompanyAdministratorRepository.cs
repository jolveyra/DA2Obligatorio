using DataAccess.Context;
using Domain;
using RepositoryInterfaces;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories
{
    public class ConstructorCompanyAdministratorRepository : IConstructorCompanyAdministratorRepository
    {
        private readonly BuildingBossContext _context;

        public ConstructorCompanyAdministratorRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public ConstructorCompanyAdministrator CreateConstructorCompanyAdministrator(ConstructorCompanyAdministrator administrator)
        {
            _context.Users.Add(administrator);
            _context.SaveChanges();

            return administrator;
        }

        public IEnumerable<ConstructorCompanyAdministrator> GetAllConstructorCompanyAdministrators()
        {
            return _context.Users.OfType<ConstructorCompanyAdministrator>();
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

        public ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(ConstructorCompanyAdministrator constructorCompanyAdministrator)
        {
            _context.Users.Update(constructorCompanyAdministrator);
            _context.SaveChanges();

            return constructorCompanyAdministrator;
        }
    }
}
