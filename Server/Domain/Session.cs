namespace Domain
{
    public class Session
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }

        public override bool Equals(object obj)
        {
            return obj is Session s && s.Id == Id && s.UserId == UserId;
        }
    }
}
