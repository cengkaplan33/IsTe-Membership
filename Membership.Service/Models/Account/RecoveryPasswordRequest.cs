namespace Membership.Service.Models.Account
{
    public class RecoveryPasswordRequest
    {
        public int ApplicationId { get; set; }

        public string Email { get; set; }
    }
}