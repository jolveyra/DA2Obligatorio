namespace CustomExceptions
{
    public class BuildingException(string message): BusinessLogicException(message)
    {
    }
}
