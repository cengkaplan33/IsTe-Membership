namespace Membership.Service.Models.Account
{
    public class CheckEmailResult
    {
        public string Message { get; set; }

        public bool IsEmailSuitable { get; set; }

        public bool IsSuccess { get; set; }
    }
}