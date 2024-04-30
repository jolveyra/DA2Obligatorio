namespace Domain
{
    public class Flat
    {
        public Guid Id { get; set; }
        public Building Building { get; set; }
        public int Number { get; set; } = 0;
        public int Floor { get; set; } = 0;
        public string OwnerName { get; set; } = "";
        public string OwnerSurname { get; set; } = "";
        public string OwnerEmail { get; set; } = "";
        public int Bathrooms { get; set; } = 0;
        public bool HasBalcony { get; set; } = false;
    }
}