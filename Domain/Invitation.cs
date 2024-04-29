namespace Domain
{
    public class Invitation
    {
        public static readonly string DefaultPassword = "Default1234";

        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public DateTime ExpirationDate { get; set; }
        public bool IsAnswered { get; set; }
        public bool IsAccepted { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Invitation i && i.Id == Id;
        }
    }
}
