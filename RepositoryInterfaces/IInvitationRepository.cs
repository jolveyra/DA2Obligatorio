using Domain;

namespace RepositoryInterfaces
{
    public interface IInvitationRepository
    {
        public IEnumerable<Invitation> GetAllInvitations();
    }
}
