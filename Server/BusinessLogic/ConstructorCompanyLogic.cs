using Domain;
using RepositoryInterfaces;
using CustomExceptions;
using LogicInterfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessLogic
{
    public class ConstructorCompanyLogic: IConstructorCompanyLogic
    {
        private IConstructorCompanyRepository _constructorCompanyRepository;
        private IUserRepository _userRepository;

        public ConstructorCompanyLogic(IConstructorCompanyRepository constructorCompanyRepository, IUserRepository userRepository)
        {
            _constructorCompanyRepository = constructorCompanyRepository;
            _userRepository = userRepository;
        }

        public ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany)
        {
            CheckNotNullOrEmptyConstructorCompanyName(constructorCompany.Name);
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

        private void CheckNotNullOrEmptyConstructorCompanyName(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new ConstructorCompanyException("Constructor company name cannot be empty");
            }
        }

        public IEnumerable<ConstructorCompany> GetAllConstructorCompanies()
        {
            return _constructorCompanyRepository.GetAllConstructorCompanies();
        }

        public ConstructorCompany GetConstructorCompanyById(Guid id)
        {
            return _constructorCompanyRepository.GetConstructorCompanyById(id);
        }

        public ConstructorCompany UpdateConstructorCompany(string newName, Guid userId, Guid constructoCompanyId)
        {
            CheckUserIsConstructorCompanyAdministrator(userId, constructoCompanyId);
            CheckNotNullOrEmptyConstructorCompanyName(newName);

            ConstructorCompany constructorCompany = GetConstructorCompanyById(constructoCompanyId);
            constructorCompany.Name = newName;

            CheckUniqueConstructorCompanyName(constructorCompany);

            return _constructorCompanyRepository.UpdateConstructorCompany(constructorCompany);
        }

        private void CheckUserIsConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            if (!(_userRepository.GetConstructorCompanyAdministratorByUserId(userId).ConstructorCompanyId == constructorCompanyId))
            {
                throw new ConstructorCompanyException("User is not an administrator of the constructor company");
            }
        }
    }
}
