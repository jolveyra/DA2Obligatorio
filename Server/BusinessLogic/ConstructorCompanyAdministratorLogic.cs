using RepositoryInterfaces;
using Domain;
using CustomExceptions;
using LogicInterfaces;

namespace BusinessLogic
{
    public class ConstructorCompanyAdministratorLogic: IConstructorCompanyAdministratorLogic
    {
        private readonly IUserRepository _iUserRepository;
        private readonly IConstructorCompanyRepository _iConstructorCompanyRepository;
        private readonly IConstructorCompanyAdministratorRepository _iConstructorCompanyAdministratorRepository;

        public ConstructorCompanyAdministratorLogic(IUserRepository iUserRepository, IConstructorCompanyRepository iConstructorCompanyRepository, IConstructorCompanyAdministratorRepository iConstructorCompanyAdministrator)
        {
            _iUserRepository = iUserRepository;
            _iConstructorCompanyRepository = iConstructorCompanyRepository;
            _iConstructorCompanyAdministratorRepository = iConstructorCompanyAdministrator;
        }

        public ConstructorCompanyAdministrator GetConstructorCompanyAdministrator(Guid userId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iUserRepository.GetConstructorCompanyAdministratorByUserId(userId);

            return constructorCompanyAdministrator;
        }

        public ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iUserRepository.GetConstructorCompanyAdministratorByUserId(userId);

            if (constructorCompanyAdministrator.ConstructorCompanyId != Guid.Empty)
                throw new ConstructorCompanyAdministratorException("Administrator is already a member from a constructor company");

            ConstructorCompany constructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            constructorCompanyAdministrator.ConstructorCompanyId = constructorCompany.Id;

            return _iConstructorCompanyAdministratorRepository.UpdateConstructorCompanyAdministrator(constructorCompanyAdministrator);
        }
    }
}