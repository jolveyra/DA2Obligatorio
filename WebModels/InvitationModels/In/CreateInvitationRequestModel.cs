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
            return new Invitation()
            {
                Name = Name,
                Email = Email,
                ExpirationDate = new DateTime(DateTime.Now.Year, DateTime.Now.Month, DateTime.Now.Day).AddDays(DaysToExpiration+1)
            };
        }
    }
}
