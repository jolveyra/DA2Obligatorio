using Domain;

namespace WebModels.UserModels
{
    public class UserResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public UserResponseModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
        }

        public override bool Equals(object? obj)
        {
            return obj is UserResponseModel u && u.Id == Id;
        }
    }
}
