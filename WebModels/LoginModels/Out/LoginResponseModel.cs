namespace WebModels.LoginModels
{
    public class LoginResponseModel
    {
        public Guid Token { get; set; }

        public LoginResponseModel(Guid token)
        {
            Token = token;
        }
    }
}
