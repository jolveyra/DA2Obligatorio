using RepositoryInterfaces;
using Domain;

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

        public ConstructorCompanyAdministrator SetConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorById(userId);

            ConstructorCompany constructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            constructorCompanyAdministrator.ConstructorCompany = constructorCompany;

            return _iConstructorCompanyAdministratorRepository.SetConstructorCompanyAdministrator(constructorCompanyAdministrator);
        }

    }
}