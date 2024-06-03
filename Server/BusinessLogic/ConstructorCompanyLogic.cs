using Domain;
using RepositoryInterfaces;
using CustomExceptions;
using LogicInterfaces;

namespace BusinessLogic
{
    public class ConstructorCompanyLogic: IConstructorCompanyLogic
    {
        private IConstructorCompanyRepository _constructorCompanyRepository;
        private IConstructorCompanyAdministratorRepository _constructorCompanyAdministratorRepository;

        public ConstructorCompanyLogic(IConstructorCompanyRepository constructorCompanyRepository, IConstructorCompanyAdministratorRepository constructorCompanyAdministratorRepository)
        {
            _constructorCompanyRepository = constructorCompanyRepository;
            _constructorCompanyAdministratorRepository = constructorCompanyAdministratorRepository;
        }

        public ConstructorCompany CreateConstructorCompany(ConstructorCompany constructorCompany, Guid administratorId)
        {
            ConstructorCompanyAdministrator administrator = _constructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(administratorId);
            ConstructorCompany administratorConstructorCompany = _constructorCompanyRepository.GetConstructorCompanyById(administrator.ConstructorCompanyId);


            if (!string.IsNullOrEmpty(administratorConstructorCompany.Name))
            {
                throw new ConstructorCompanyException("Administrator already belongs to a constructor company");
            }

            CheckNotNullOrEmptyConstructorCompanyName(constructorCompany.Name);
            CheckUniqueConstructorCompanyName(constructorCompany.Name);
            
            administrator.ConstructorCompany.Name = constructorCompany.Name;

            return _constructorCompanyRepository.CreateConstructorCompany(constructorCompany);
        }

        private void CheckUniqueConstructorCompanyName(string constructorCompanyName)
        {
            if (GetAllConstructorCompanies().Any(c => c.Name == constructorCompanyName))
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
            CheckUniqueConstructorCompanyName(newName);

            ConstructorCompany constructorCompany = GetConstructorCompanyById(constructoCompanyId);
            constructorCompany.Name = newName;

            return _constructorCompanyRepository.UpdateConstructorCompany(constructorCompany);
        }

        private void CheckUserIsConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            if (!(_constructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId).ConstructorCompanyId == constructorCompanyId))
            {
                throw new ConstructorCompanyException("User is not an administrator of the constructor company");
            }
        }
    }
}
