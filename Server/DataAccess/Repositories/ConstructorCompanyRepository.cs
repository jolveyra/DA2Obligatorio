using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class ConstructorCompanyRepository : IConstructorCompanyRepository
    {

        private readonly BuildingBossContext _context;

        public ConstructorCompanyRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany)
        {
            _context.ConstructorCompanies.Add(constructorCompany);
            _context.SaveChanges();
            return constructorCompany;
        }

        public void DeleteConstructorCompany(ConstructorCompany constructorCompany)
        {
            _context.ConstructorCompanies.Remove(constructorCompany);
            _context.SaveChanges();
        }

        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies()
        {
            return _context.ConstructorCompanies;

        }

        public ConstructorCompany GetConstructorCompanyById(Guid guid)
        {
            return _context.ConstructorCompanies.FirstOrDefault(c => c.Id == guid);
        }

        public ConstructorCompany UpdateConstructorCompany(ConstructorCompany constructorCompany)
        {
            _context.ConstructorCompanies.Update(constructorCompany);
            _context.SaveChanges();
            return constructorCompany;
        }
    }
}
