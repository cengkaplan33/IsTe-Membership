namespace Membership.Service.Models.Authentication
{
    public class LogoutRequest
    {
        public int ApplicationId { get; set; }

        public string AccountCode { get; set; }
    }
}