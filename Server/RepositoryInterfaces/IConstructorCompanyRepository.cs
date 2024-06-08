using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain;

namespace RepositoryInterfaces
{
    public interface IConstructorCompanyRepository
    {
        ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany);
        void DeleteConstructorCompany(ConstructorCompany constructorCompany);
        IEnumerable<ConstructorCompany> GetAllConstructorCompanies();
        ConstructorCompany GetConstructorCompanyById(Guid id);
        ConstructorCompany UpdateConstructorCompany(ConstructorCompany constructorCompany);
    }
}
