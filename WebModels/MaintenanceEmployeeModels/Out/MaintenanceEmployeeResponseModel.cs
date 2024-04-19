using Domain;

namespace WebModels.MaintenanceEmployeeModels
{
    public class MaintenanceEmployeeResponseModel
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Email { get; set; }

        public MaintenanceEmployeeResponseModel(User user)
        {
            Id = user.Id;
            Name = user.Name;
            Surname = user.Surname;
            Email = user.Email;
        }
        
        public override bool Equals(object? obj)
        {
            return obj is MaintenanceEmployeeResponseModel m && m.Id == Id;
        }
    }
}
