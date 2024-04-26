using DataAccess.Context;
using Domain;
using RepositoryInterfaces;

namespace DataAccess.Repositories
{
    public class InvitationRepository : IInvitationRepository
    {
        private readonly BuildingBossContext _context;

        public InvitationRepository(BuildingBossContext context)
        {
            _context = context;
        }

        public Invitation CreateInvitation(Invitation invitation)
        {
            throw new NotImplementedException();
        }

        public void DeleteInvitation(Guid id)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<Invitation> GetAllInvitations()
        {
            return _context.Invitations;
        }

        public Invitation GetInvitationById(Guid id)
        {
            Invitation? invitation = _context.Invitations.FirstOrDefault(i => i.Id == id);

            if (invitation == null)
            {
                throw new ArgumentException("Invitation not found");
            }

            return invitation;
        }

        public Invitation UpdateInvitationStatus(Guid id, bool isAccepted)
        {
            throw new NotImplementedException();
        }
    }
}
