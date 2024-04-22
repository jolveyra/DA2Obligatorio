namespace ManagementApi.CustomExceptions
{
    public class ResourceNotFoundException(string message) : Exception(message);
}
