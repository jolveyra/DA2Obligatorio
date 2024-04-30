namespace Domain
{
    public class User : Person
    {
        public string Password { get; set; }
        public Role Role { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is User u && u.Id == Id;
        }
    }
}
