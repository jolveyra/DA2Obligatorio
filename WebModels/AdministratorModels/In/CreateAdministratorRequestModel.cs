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
            if (string.IsNullOrEmpty(Name) || string.IsNullOrEmpty(Surname) || string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                throw new MissingFieldException("There is a missing field in the request's body");
            }

            return new User
            {
                Name = Name,
                Surname = Surname,
                Email = Email,
                Password = Password,
                Role = Role.Administrator
            };
        }
    }
}
