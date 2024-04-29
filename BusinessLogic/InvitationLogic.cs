using Domain;
using LogicInterfaces;
using RepositoryInterfaces;
using CustomExceptions.BusinessLogic;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;

        public InvitationLogic(IInvitationRepository invitationRepository, IUserRepository userRepository)
        {
            _invitationRepository = invitationRepository;
            _userRepository = userRepository;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            ValidateInvitation(invitation);

            if (_userRepository.GetAllUsers().Any(u => u.Email == invitation.Email))
            {
                throw new InvitationException("There is already a user with the same email");
            }

            return _invitationRepository.CreateInvitation(invitation);
        }

        public void DeleteInvitation(Guid id)
        {
            Invitation invitation = GetInvitationById(id);

            if ((invitation.IsAnswered && !invitation.IsAccepted) || invitation.ExpirationDate < DateTime.Now)
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

        public Invitation UpdateInvitationStatus(Guid id, bool isAccepted)
        {
            Invitation invitation = GetInvitationById(id);
            invitation.IsAccepted = isAccepted;
            return _invitationRepository.UpdateInvitation(invitation);
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
    }
}
