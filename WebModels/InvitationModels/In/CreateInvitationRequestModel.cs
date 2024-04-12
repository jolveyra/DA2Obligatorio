using Domain;

namespace WebModels.InvitationModels
{
    public class CreateInvitationRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int DaysToExpiration { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation(Name, Email, DaysToExpiration);
        }
    }
}
