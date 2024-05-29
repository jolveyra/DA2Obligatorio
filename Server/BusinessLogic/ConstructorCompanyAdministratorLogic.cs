using RepositoryInterfaces;
using Domain;
using CustomExceptions;

namespace BusinessLogic
{
    public class ConstructorCompanyAdministratorLogic
    {
        private readonly IConstructorCompanyAdministratorRepository _iConstructorCompanyAdministratorRepository;
        private readonly IConstructorCompanyRepository _iConstructorCompanyRepository;

        public ConstructorCompanyAdministratorLogic(IConstructorCompanyAdministratorRepository iConstructorCompanyAdministratorRepository, IConstructorCompanyRepository iConstructorCompanyRepository)
        {
            _iConstructorCompanyAdministratorRepository = iConstructorCompanyAdministratorRepository;
            _iConstructorCompanyRepository = iConstructorCompanyRepository;
        }

        public ConstructorCompany GetAdminConstructorCompany(Guid userId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorById(userId);

            return constructorCompanyAdministrator.ConstructorCompany;
        }

        public ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorById(userId);

            if (constructorCompanyAdministrator.ConstructorCompany is not null)
                throw new ConstructorCompanyAdministratorException("Administrator is already a member from a constructor company");

            ConstructorCompany constructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            constructorCompanyAdministrator.ConstructorCompany = constructorCompany;

            return _iConstructorCompanyAdministratorRepository.SetConstructorCompanyAdministrator(constructorCompanyAdministrator);
        }

    }
}