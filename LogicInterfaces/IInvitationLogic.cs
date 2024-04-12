using Domain;

namespace LogicInterfaces
{
    public interface IInvitationLogic
    {
        Invitation CreateInvitation(Invitation invitation);
        IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
    }
}
