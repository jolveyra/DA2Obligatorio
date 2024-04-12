namespace Domain
{
    public class Invitation
    {
        public Guid Id { get; private set; } = Guid.NewGuid();
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; private set; } = DateTime.Now;
        public bool Accepted { get; set; } = false;
    }
}
