namespace Domain
{
    public class Request
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid BuildingId { get; set; }
        public Guid FlatId { get; set; }
        public Category Category { get; set; }
        public Guid AssignedEmployeeId { get; set; }
    }
}
