using Domain;

namespace WebModels
{
    public class InvitationResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string expirationDate { get; set; }
        public bool accepted { get; set; }

        public InvitationResponseModel(Invitation invitation)
        {
            Id = invitation.Id;
            Name = invitation.Name;
            Email = invitation.Email;
            expirationDate = invitation.ExpirationDate.ToString("d/M/yyyy");
            accepted = invitation.Accepted;
        }

        public override bool Equals(object? obj)
        {
            return obj is InvitationResponseModel i && i.Id == Id;
        }
    }
}
