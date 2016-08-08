namespace Membership.Service.Models.Authentication
{
    public class AuthenticationResult
    {
        public string Message { get; set; }

        public AuthenticationStatus Status { get; set; }
    }

    public enum AuthenticationStatus
    {
        Successful = 1,

        InvalidEmail = 2,

        InvalidPassword = 3,

        AccountDisable = 4,

        InvalidIp = 5
    }
}