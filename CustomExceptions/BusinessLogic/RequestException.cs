namespace CustomExceptions.BusinessLogic
{
    public class RequestException(string message) : Exception(message);
}
