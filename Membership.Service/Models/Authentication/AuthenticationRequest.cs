namespace Membership.Service.Models.Authentication
{
    public class AuthenticationRequest
    {
        public int ApplicationId { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string RequestIp { get; set; }
    }
}