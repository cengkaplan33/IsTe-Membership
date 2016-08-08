namespace Membership.Api.Models.Account
{
    public class CheckEmailRequestModel
    {
        /// <summary>
        ///     Uygulama Id'si
        /// </summary>
        public int ApplicationId { get; set; }

        public string Email { get; set; }
    }
}