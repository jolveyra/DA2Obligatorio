namespace CustomExceptions
{
    public class RequestException(string message) : BusinessLogicException(message);
}
