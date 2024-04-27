using Domain;

namespace RepositoryInterfaces
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        void DeleteInvitationById(Guid id);
        public IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
        Invitation UpdateInvitation(Invitation invitation);
    }
}
