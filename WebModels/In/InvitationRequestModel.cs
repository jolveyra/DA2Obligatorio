using Domain;

namespace WebModels
{
    public class InvitationRequestModel
    {
        public string Name { get; set; }
        public string Email { get; set; }

        public Invitation ToEntity()
        {
            return new Invitation { Name = Name, Email = Email };
        }
    }
}
