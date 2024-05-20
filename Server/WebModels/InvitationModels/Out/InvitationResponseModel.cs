using Domain;

namespace WebModels.InvitationModels
{
    public class InvitationResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string ExpirationDate { get; set; }
        public bool IsAccepted { get; set; }
        public bool IsAnswered { get; set; }

        public InvitationResponseModel(Invitation invitation)
        {
            Id = invitation.Id;
            Name = invitation.Name;
            Email = invitation.Email;
            ExpirationDate = invitation.ExpirationDate.ToString("d/M/yyyy");
            IsAccepted = invitation.IsAccepted;
            IsAnswered = invitation.IsAnswered;
        }

        public override bool Equals(object? obj)
        {
            return obj is InvitationResponseModel i && i.Id == Id;
        }
    }
}
