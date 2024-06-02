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

            if (!String.IsNullOrEmpty(constructorCompanyAdministrator.ConstructorCompany.Name))
                throw new ConstructorCompanyAdministratorException("Administrator is already a member from a constructor company");

            ConstructorCompany constructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            ConstructorCompany toDeleteConstructorCompany = constructorCompanyAdministrator.ConstructorCompany;

            constructorCompanyAdministrator.ConstructorCompanyId = constructorCompany.Id;
            constructorCompanyAdministrator.ConstructorCompany = constructorCompany;

            _iConstructorCompanyRepository.DeleteConstructorCompany(toDeleteConstructorCompany);

            return _iConstructorCompanyAdministratorRepository.UpdateConstructorCompanyAdministrator(constructorCompanyAdministrator);
        }

        public static ConstructorCompanyAdministrator CreateConstructorCompanyAdmin(IUserRepository userRepository, ISessionRepository sessionRepository, IConstructorCompanyAdministratorRepository constructorCompanyAdministratorRepository, ConstructorCompanyAdministrator constructorCompanyAdministrator)
        {
            constructorCompanyAdministrator.Role = Role.ConstructorCompanyAdmin;
            constructorCompanyAdministrator.Surname = "";
            UserLogic.ValidateUser(constructorCompanyAdministrator);

            if (UserLogic.ExistsUserEmail(userRepository, constructorCompanyAdministrator.Email))
            {
                throw new UserException("A user with the same email already exists");
            }

            ConstructorCompanyAdministrator newUser = constructorCompanyAdministratorRepository.CreateConstructorCompanyAdministrator(constructorCompanyAdministrator);

            sessionRepository.CreateSession(new Session() { UserId = newUser.Id });

            return newUser;

        }

        public Guid GetConstructorCompanyByUserId(Guid userId)
        {
            return _iUserRepository.GetConstructorCompanyAdministratorByUserId(userId).ConstructorCompanyId;
        }

    }
}