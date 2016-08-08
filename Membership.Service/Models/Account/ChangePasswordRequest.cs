namespace Membership.Service.Models.Account
{
    public class ChangePasswordRequest
    {
        public int ApplicationId { get; set; }

        public string AccountCode { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }
    }
}