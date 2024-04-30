namespace CustomExceptions
{
    public class UserException(string message) : BusinessLogicException(message);
}
