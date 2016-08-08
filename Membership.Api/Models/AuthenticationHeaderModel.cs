namespace Membership.Api.Models
{
    public class ApiKeyAuthenticationHeaderModel
    {
        public string ApiKey { get; set; }
        public string SecretKey { get; set; }
    }

    public class TokenAuthenticationHeaderModel
    {
        public string Token { get; set; }        
    }
}