using Domain;

namespace RepositoryInterfaces
{
    public interface IInvitationRepository
    {
        Invitation CreateInvitation(Invitation invitation);
        public IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
    }
}
