namespace CustomExceptions
{
    public class InvitationException(string message) : BusinessLogicException(message)
    {
    }
}
