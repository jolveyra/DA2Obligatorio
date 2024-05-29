using RepositoryInterfaces;
using Domain;
using CustomExceptions;

namespace BusinessLogic
{
    public class ConstructorCompanyAdministratorLogic
    {
        private readonly IConstructorCompanyAdministratorRepository _iConstructorCompanyAdministratorRepository;
        private readonly IConstructorCompanyRepository _iConstructorCompanyRepository;
        private readonly IUserRepository _iUserRepository;

        public ConstructorCompanyAdministratorLogic(IConstructorCompanyAdministratorRepository iConstructorCompanyAdministratorRepository, IConstructorCompanyRepository iConstructorCompanyRepository, IUserRepository iUserRepository)
        {
            _iConstructorCompanyAdministratorRepository = iConstructorCompanyAdministratorRepository;
            _iConstructorCompanyRepository = iConstructorCompanyRepository;
            _iUserRepository = iUserRepository;
        }

        public ConstructorCompany GetAdminConstructorCompany(Guid userId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iUserRepository.GetConstructorCompanyAdministratorByUserId(userId);

            if (constructorCompanyAdministrator.ConstructorCompany is null)
                throw new ConstructorCompanyAdministratorException("Administrator is not a member from a constructor company");

            return constructorCompanyAdministrator.ConstructorCompany;
        }

        public ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iUserRepository.GetConstructorCompanyAdministratorByUserId(userId);

            if (constructorCompanyAdministrator.ConstructorCompany is not null)
                throw new ConstructorCompanyAdministratorException("Administrator is already a member from a constructor company");

            ConstructorCompany constructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            constructorCompanyAdministrator.ConstructorCompany = constructorCompany;

            return _iConstructorCompanyAdministratorRepository.SetConstructorCompanyAdministrator(constructorCompanyAdministrator);
        }

    }
}