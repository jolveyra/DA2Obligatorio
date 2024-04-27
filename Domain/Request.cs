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
        public RequestStatus Status { get; set; }


        public override bool Equals(object obj)
        {
            return obj is Request r && r.Id == Id;
        }
    }
}
