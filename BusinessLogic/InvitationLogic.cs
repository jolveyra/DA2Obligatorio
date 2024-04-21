using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

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
            try
            {
                _userRepository.GetUserByEmail(invitation.Email);
            }
            catch (ArgumentException)
            {
                return _invitationRepository.CreateInvitation(invitation);
            }

            throw new ArgumentException("There is already a user with the same email");
        }

        public void DeleteInvitation(Guid id)
        {
            _invitationRepository.DeleteInvitation(id);
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
            try
            {
                return _invitationRepository.UpdateInvitationStatus(id, isAccepted);
            }
            catch (ArgumentException)
            {
                throw new ArgumentException("There is no invitation with that id.");
            }
        }
    }
}
