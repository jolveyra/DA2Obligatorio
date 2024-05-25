using Domain;
using RepositoryInterfaces;
using CustomExceptions;
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
            CheckUniqueConstructorCompanyName(constructorCompany);
            return _constructorCompanyRepository.CreateConstructorCompany(constructorCompany);
        }

        private void CheckUniqueConstructorCompanyName(ConstructorCompany constructorCompany)
        {
            if (GetAllConstructorCompanies().Any(c => c.Name == constructorCompany.Name))
            {
                throw new ConstructorCompanyException("Constructor company with same name already exists");
            }
        }

        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies()
        {
            return _constructorCompanyRepository.GetAllConstructorCompanies();
        }
    }
}
