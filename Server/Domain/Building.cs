namespace Domain
{
    public class Building
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public float SharedExpenses { get; set; }
        public Address Address { get; set; }
        public Guid ConstructorCompanyId { get; set; }
        public User Manager { get; set; }
        public List<Guid> MaintenanceEmployees { get; set; } = new List<Guid>();

        public override bool Equals(object? obj)
        {
            return obj is Building b && b.Id == Id;
        }
    }
}