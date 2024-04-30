namespace Domain
{
    public class Request
    {
        public Guid AssignedEmployeeId;

        public Guid Id { get; set; }
        public string Description { get; set; }
        public Flat Flat { get; set; }
        public Category Category { get; set; }
        public User AssignedEmployee { get; set; }
        public RequestStatus Status { get; set; }
        public DateTime StartingDate { get; set; }
        public DateTime CompletionDate { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Request r && r.Id == Id;
        }
    }
}
