using CustomExceptions.DataAccessExceptions;
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
            _context.Invitations.Add(invitation);
            _context.SaveChanges();
            return invitation;
        }

        public void DeleteInvitationById(Guid id)
        {
            Invitation? invitation = _context.Invitations.FirstOrDefault(i => i.Id == id);

            if (invitation == null)
            {
                throw new DeleteException();
            }

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

        public Invitation UpdateInvitation(Invitation invitation)
        {
            _context.Invitations.Update(invitation);
            _context.SaveChanges();

            return invitation;
        }
    }
}
