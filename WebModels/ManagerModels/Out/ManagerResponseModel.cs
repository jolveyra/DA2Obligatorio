using Domain;

namespace WebModels.ManagerModels
{
    public class ManagerResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string Surname { get; set; }

        public ManagerResponseModel(User manager)
        {
            Id = manager.Id;
            Name = manager.Name;
            Email = manager.Email;
            Surname = manager.Surname;
        }

        public override bool Equals(object? obj)
        {
            return obj is ManagerResponseModel m && m.Id == Id;
        }
    }
}
