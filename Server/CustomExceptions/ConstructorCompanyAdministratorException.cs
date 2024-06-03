namespace CustomExceptions
{
    public class ConstructorCompanyAdministratorException(string message) : BusinessLogicException(message)
    {
    }
}