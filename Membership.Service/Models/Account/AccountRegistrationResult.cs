namespace Membership.Service.Models.Account
{
    public class AccountRegistrationResult
    {     
        public string Message { get; set; }

        public AccountRegistrationStatus Status { get; set; }
    }

    public enum AccountRegistrationStatus
    {
        Successful = 1,

        Invalid = 2
    }
}