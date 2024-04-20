using Domain;
using LogicInterfaces;
using RepositoryInterfaces;

namespace BusinessLogic
{
    public class InvitationLogic : IInvitationLogic
    {
        private readonly IInvitationRepository _invitationRepository;
        private readonly IUserRepository _userRepository;

        public InvitationLogic(IInvitationRepository invitationRepository)
        {
            _invitationRepository = invitationRepository;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            return _invitationRepository.CreateInvitation(invitation);
        }

        public void DeleteInvitation(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _invitationRepository.GetAllInvitations();
        }

        public Invitation GetInvitationById(Guid id)
        {
            throw new NotImplementedException();
        }

        public Invitation UpdateInvitationStatus(Guid id, bool isAccepted)
        {
            throw new NotImplementedException();
        }
    }
}
