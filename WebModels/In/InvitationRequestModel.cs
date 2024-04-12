using Domain;

namespace WebModels
{
    public class InvitationRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public bool Accepted { get; set; }
        public int DaysToExpiration { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation { Name = Name, Email = Email, Accepted = Accepted };
        }
    }
}
