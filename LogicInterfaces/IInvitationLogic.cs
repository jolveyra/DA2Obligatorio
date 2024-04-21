using Domain;

namespace LogicInterfaces
{
    public interface IInvitationLogic
    {
        Invitation CreateInvitation(Invitation invitation);
        void DeleteInvitation(Guid id);
        IEnumerable<Invitation> GetAllInvitations();
        Invitation GetInvitationById(Guid id);
        Invitation UpdateInvitationStatus(Guid id, bool isAccepted);
    }
}
