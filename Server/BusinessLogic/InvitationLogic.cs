using Domain;
using LogicInterfaces;
using RepositoryInterfaces;
using CustomExceptions;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;
        private readonly ISessionRepository _sessionRepository;
        private readonly IConstructorCompanyAdministratorRepository _constructorCompanyAdministratorRepository;

        public InvitationLogic(IInvitationRepository invitationRepository, IUserRepository userRepository, ISessionRepository sessionRepository, IConstructorCompanyAdministratorRepository constructorCompanyAdministratorRepository)
        {
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
            _sessionRepository = sessionRepository;
            _constructorCompanyAdministratorRepository = constructorCompanyAdministratorRepository;
        }

        public Invitation CreateInvitation(Invitation invitation, string role)
        {
            ValidateRole(invitation, role);
            ValidateInvitation(invitation);
            ValidateInvitationEmail(invitation.Email);

            return _invitationRepository.CreateInvitation(invitation);
        }

        public void DeleteInvitation(Guid id)
        {
            Invitation invitation = _invitationRepository.GetAllInvitations().ToList().FirstOrDefault(i => i.Id == id);
            
            if(invitation is null)
            {
                throw new DeleteException();
            }

            if ((invitation.IsAnswered && !invitation.IsAccepted) || (invitation.ExpirationDate < DateTime.Now && !invitation.IsAnswered))
            {
                _invitationRepository.DeleteInvitationById(id);
            }
            else
            {
                throw new InvitationException("The invitation cannot be deleted");
            }
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }

        public Invitation GetInvitationById(Guid id)
        {
            return _invitationRepository.GetInvitationById(id);
        }

        public Invitation UpdateInvitationStatus(Guid id, bool? isAccepted)
        {
            if (isAccepted is null)
            {
                throw new InvitationException("The field isAccepted is missing in the body of the request");
            }

            Invitation invitation = RetrieveUpdatableInvitation(id);

            invitation.IsAnswered = true;
            invitation.IsAccepted = (bool)isAccepted;

            if ((bool)isAccepted)
            {
                CreateUser(invitation);
            }

            return _invitationRepository.UpdateInvitation(invitation);
        }

        private void CreateUser(Invitation invitation)
        {

            if (invitation.Role == InvitationRole.Manager)
            {
                User user = new User() { Name = invitation.Name, Email = invitation.Email, Password = Invitation.DefaultPassword };
                UserLogic.CreateManager(_userRepository, _sessionRepository, user);
            }
            else
            {
                ConstructorCompanyAdministrator administrator = new ConstructorCompanyAdministrator() { Name = invitation.Name, Email = invitation.Email, Password = Invitation.DefaultPassword };
                ConstructorCompanyAdministratorLogic.CreateConstructorCompanyAdmin(_userRepository, _sessionRepository, _constructorCompanyAdministratorRepository, administrator);
            }
        }

        private Invitation RetrieveUpdatableInvitation(Guid id)
        {
            Invitation invitation = GetInvitationById(id);

            if (invitation.IsAnswered)
            {
                throw new InvitationException("The invitation has already been answered");
            }

            return invitation;
        }

        private static void ValidateInvitation(Invitation invitation)
        {
            if (!UserLogic.isValidEmail(invitation.Email))
            {
                throw new InvitationException("An Email must contain '@', '.' and be longer than 4 characters long");
            }

            if (string.IsNullOrEmpty(invitation.Name))
            {
                throw new InvitationException("The Name field cannot be empty");
            }

            if (invitation.ExpirationDate <= DateTime.Today)
            {
                throw new InvitationException("The date of expiration must be from tomorrow onwards");
            }
        }

        private void ValidateInvitationEmail(string email)
        {
            if (UserLogic.ExistsUserEmail(_userRepository, email))
            {
                throw new InvitationException("There is already a user with the same email");
            }
        }

        private void ValidateRole(Invitation invitation, string role)
        {
            if (string.IsNullOrEmpty(role))
            {
                throw new InvitationException("The Role field cannot be empty");
            }

            role = role.ToLower();

            if (role != "constructorcompanyadmin" && role != "manager")
            {
                throw new InvitationException("The Role field must be either 'ConstructorCompanyAdmin' or 'Manager'");
            }

            if (role == "constructorcompanyadmin")
            {
                invitation.Role = InvitationRole.ConstructorCompanyAdmin;
            }
            else
            {
                invitation.Role = InvitationRole.Manager;
            }
        }
    }
}
