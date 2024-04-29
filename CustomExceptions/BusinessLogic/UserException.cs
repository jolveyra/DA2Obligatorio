namespace CustomExceptions.BusinessLogic
{
    public class UserException(string message) : BusinessLogicException(message);
}
