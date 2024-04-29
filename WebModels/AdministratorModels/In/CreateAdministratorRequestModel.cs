using Domain;

namespace WebModels.AdministratorModels
{
    public class CreateAdministratorRequestModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Password = Password
            };
        }
    }
}
