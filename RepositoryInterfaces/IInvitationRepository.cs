using Domain;

namespace RepositoryInterfaces
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        void DeleteInvitation(Guid id);
        public IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
        Invitation UpdateInvitationStatus(Guid id, bool isAccepted);
    }
}
