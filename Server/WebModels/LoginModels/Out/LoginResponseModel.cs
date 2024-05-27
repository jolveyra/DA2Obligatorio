using Domain;

namespace WebModels.LoginModels
{
    public class LoginResponseModel
    {
        public Guid Token { get; set; }
        public string Name { get; set; }
        public string Role { get; set; }

        public LoginResponseModel(UserLogged userLogged)
        {
            Token = userLogged.Token;
            Name = userLogged.Name;
            Role = userLogged.Role.ToString();
        }
    }
}
