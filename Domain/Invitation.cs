namespace Domain
{
    public class Invitation
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; private set; }
        public bool IsAccepted { get; set; }

        public Invitation(string name, string email, int daysToExpiration)
        {
            Id = Guid.NewGuid();
            Name = name;
            Email = email;
            ExpirationDate = DateTime.Now.AddDays(daysToExpiration);
            IsAccepted = false;
        }
    }
}
