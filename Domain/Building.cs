namespace Domain
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public List<Flat> Flats { get; set; }
        public string Street { get; set; }
        public int DoorNumber { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string CornerStreet { get; set; }

        public string ConstructorCompany { get; set; }
    }
}