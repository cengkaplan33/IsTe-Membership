namespace Membership.Service.Models.Account
{
    public class CheckEmailRequest
    {
        public int ApplicationId { get; set; }

        public string Email { get; set; }
    }
}