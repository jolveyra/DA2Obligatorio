using Domain;

namespace WebModels.AdministratorModels
{
    public class AdministratorResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public AdministratorResponseModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
        }

        public override bool Equals(object? obj)
        {
            return obj is AdministratorResponseModel a && a.Id == Id;
        }
    }
}
