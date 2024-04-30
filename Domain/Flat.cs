namespace Domain
{
    public class Flat
    {
        public Guid Id { get; set; }
        public Building Building { get; set; }
        public int Number { get; set; } = 0;
        public int Floor { get; set; } = 0;
        public Person Owner { get; set; } = new Person();
        public Guid OwnerId { get; set; }
        public int Rooms { get; set; } = 0;
        public int Bathrooms { get; set; } = 0;
        public bool HasBalcony { get; set; } = false;

        public override bool Equals(object? obj)
        {
            return obj is Flat f && f.Id == Id;
        }
    }
}