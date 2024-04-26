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
            if (_context.Invitations.FirstOrDefault(i => i.Id == invitation.Id) is not null)
            { // Tendria que chequearse igual??
                throw new ArgumentException("An invitation with the same id already exists");
            }

            _context.Invitations.Add(invitation);
            _context.SaveChanges();
            return invitation;
        }

        public void DeleteInvitationById(Guid id)
        {
            _context.Invitations.Remove(GetInvitationById(id));
            _context.SaveChanges();
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
