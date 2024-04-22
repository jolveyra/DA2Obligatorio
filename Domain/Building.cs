namespace Domain
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public List<Flat> Flats { get; set; }
        public string Direction { get; set; }
    }
}