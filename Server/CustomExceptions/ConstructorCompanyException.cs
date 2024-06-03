namespace CustomExceptions
{
    public class ConstructorCompanyException(string message): BusinessLogicException(message)
    {
    }
}