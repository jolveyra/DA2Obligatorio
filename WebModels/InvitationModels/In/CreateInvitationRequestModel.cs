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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Email) || DaysToExpiration == null)
                throw new ArgumentException("There is a missing field in the request's body");

            return new Invitation(Name, Email, DaysToExpiration);
        }
    }
}
