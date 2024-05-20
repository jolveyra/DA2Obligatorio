namespace Domain
{
    public class Person
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = "";
        public string Surname { get; set; } = "";
        public string Email { get; set; } = "";

        public override bool Equals(object? obj)
        {
            return obj is Person p && p.Id == Id;
        }
    }
}
