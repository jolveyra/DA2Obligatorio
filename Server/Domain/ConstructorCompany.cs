namespace Domain
{
    public class ConstructorCompany
    {
        public Guid Id { get; set; }
        public string Name { get; set; }

        public override bool Equals(object? obj)
        {
            return obj is ConstructorCompany constructorCompany && Id == constructorCompany.Id;
        }
    }
}