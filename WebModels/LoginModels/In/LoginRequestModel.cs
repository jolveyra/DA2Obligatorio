using Domain;

namespace WebModels.LoginModels
{
    public class LoginRequestModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public User ToEntity()
        {
            return new User()
            {
                Email = Email,
                Password = Password
            };
        }
    }
}
