namespace ManagementApi.CustomExceptions
{
    public class ResourceNotFoundException(string message) : System.Exception(message);
}
