namespace XMLImporterV1
{
    public class DTOBuilding
    {
        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public string Street { get; set; }
        public string CornerStreet { get; set; }
        public int Number { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        public string? ManagerEmail { get; set; }
        public List<DTOFlat> Flats { get; set; }
    }
}
