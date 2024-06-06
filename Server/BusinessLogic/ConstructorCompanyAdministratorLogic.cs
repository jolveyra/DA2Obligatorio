using RepositoryInterfaces;
using Domain;
using CustomExceptions;
using LogicInterfaces;

namespace BusinessLogic
{
    public class ConstructorCompanyAdministratorLogic: IConstructorCompanyAdministratorLogic
    {
        private readonly IConstructorCompanyRepository _iConstructorCompanyRepository;
        private readonly IConstructorCompanyAdministratorRepository _iConstructorCompanyAdministratorRepository;

        public ConstructorCompanyAdministratorLogic(IConstructorCompanyRepository iConstructorCompanyRepository, IConstructorCompanyAdministratorRepository iConstructorCompanyAdministrator)
        {
            _iConstructorCompanyRepository = iConstructorCompanyRepository;
            _iConstructorCompanyAdministratorRepository = iConstructorCompanyAdministrator;
        }

        IEnumerable<ConstructorCompanyAdministrator> IConstructorCompanyAdministratorLogic.GetAllConstructorCompanyAdministrators(Guid userId)
        {
            throw new NotImplementedException();
        }

        public ConstructorCompanyAdministrator GetConstructorCompanyAdministrator(Guid userId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId);

            return constructorCompanyAdministrator;
        }

        public ConstructorCompanyAdministrator UpdateConstructorCompanyAdministrator(Guid userId, Guid constructorCompanyId)
        {
            ConstructorCompanyAdministrator constructorCompanyAdministrator = _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId);
            ConstructorCompany administratorConstructorCompany = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyAdministrator.ConstructorCompanyId);

            if (!string.IsNullOrEmpty(administratorConstructorCompany.Name))
            {
                throw new ConstructorCompanyAdministratorException("Administrator already belongs to a constructor company");
            }

            ConstructorCompany constructorCompanyToBeAssigned = _iConstructorCompanyRepository.GetConstructorCompanyById(constructorCompanyId);

            ConstructorCompany toDeleteConstructorCompany = administratorConstructorCompany;

            constructorCompanyAdministrator.ConstructorCompanyId = constructorCompanyToBeAssigned.Id;
            constructorCompanyAdministrator.ConstructorCompany = constructorCompanyToBeAssigned;

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
            return _iConstructorCompanyAdministratorRepository.GetConstructorCompanyAdministratorByUserId(userId).ConstructorCompanyId;
        }
    }
}