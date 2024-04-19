namespace WebModels.InvitationModels
{
    public class UpdateInvitationRequestModel
    {
        public bool IsAccepted
        {
            get
            {
                if (IsAccepted == null)
                    throw new MissingFieldException("There is a missing field in the request's body");

                return IsAccepted;
            }
            set => IsAccepted = value;
        }
    }
}
