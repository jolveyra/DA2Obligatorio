namespace CustomExceptions.BusinessLogic
{
    public class RequestException(string message) : BusinessLogicException(message);
}
