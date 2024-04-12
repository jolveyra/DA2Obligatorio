using Domain;

namespace LogicInterfaces
{
    public interface IInvitationLogic
    {
        IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
    }
}
