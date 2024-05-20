namespace CustomExceptions
{
    public class CategoryException(string message) : BusinessLogicException(message)
    {
    }
}
