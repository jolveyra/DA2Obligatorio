using Domain;

namespace WebModels.InvitationModels
{
    public class UpdateInvitationResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsAccepted { get; set; }
        public string DefaultPassword { get; set; }

        public UpdateInvitationResponseModel(Invitation invitation)
        {
            Id = invitation.Id;
            Name = invitation.Name;
            Email = invitation.Email;
            ExpirationDate = invitation.ExpirationDate;
            IsAnswered = invitation.IsAnswered;
            IsAccepted = invitation.IsAccepted;
            DefaultPassword = Invitation.DefaultPassword;
        }
    }
}
