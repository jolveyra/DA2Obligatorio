namespace Domain
{
    public class Address
    {
        public Guid Id { get; set; }
        public string Street { get; set; }
        public int DoorNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CornerStreet { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Address a &&
                   a.Street.Equals(Street) &&
                   a.DoorNumber == DoorNumber &&
                   a.CornerStreet.Equals(CornerStreet);
        }
    }
}
