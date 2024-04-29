namespace Domain
{
    public class Flat
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public int Floor { get; set; }
        public string OwnerName { get; set; }
        public string OwnerSurname { get; set; }
        public string OwnerEmail { get; set; }
        public int Bathrooms { get; set; }
        public bool HasBalcony { get; set; }
        public Building Building { get; set; }
    }
}