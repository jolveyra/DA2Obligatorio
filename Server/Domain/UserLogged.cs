namespace Domain
{
    public class UserLogged
    {
        public Guid Token { get; set; }

        public string Name { get; set; }
        public Role Role { get; set; }
    }
}
