using Domain;
using RepositoryInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ConstructorCompanyLogic
    {
        private IConstructorCompanyRepository _constructorCompanyRepository;

        public ConstructorCompanyLogic(IConstructorCompanyRepository constructorCompanyRepository)
        {
            _constructorCompanyRepository = constructorCompanyRepository;
        }

        public ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany)
        {
            return _constructorCompanyRepository.CreateConstructorCompany(constructorCompany);
        }

        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies()
        {
            return _constructorCompanyRepository.GetAllConstructorCompanies();
        }
    }
}
