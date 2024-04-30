namespace Domain
{
    public class Category
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is Category c && c.Id == Id;
        }
    }
}
