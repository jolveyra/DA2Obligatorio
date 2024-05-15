using Domain;

namespace WebModels.UserModels
{
    public class UserUpdateRequestModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Name = Name,
                Surname = Surname,
                Password = Password
            };
        }
    }
}
