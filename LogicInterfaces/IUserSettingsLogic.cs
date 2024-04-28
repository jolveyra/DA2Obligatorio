using Domain;

namespace LogicInterfaces
{
    public interface IUserSettingsLogic
    {
        User GetUserById(Guid userId);
    }
}
