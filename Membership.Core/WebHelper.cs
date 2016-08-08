using System.Web;

namespace Membership.Core
{
    public class WebHelper : IWebHelper
    {
        private readonly HttpContextBase _httpContext;

        public WebHelper(HttpContextBase httpContext)
        {
            _httpContext = httpContext;
        }

        public string GetUrlReferrer()
        {
            var referrerUrl = string.Empty;

            if (_httpContext?.Request?.UrlReferrer != null)
                referrerUrl = _httpContext.Request.UrlReferrer.ToString();

            return referrerUrl;
        }

        public string GetCurrentIpAddress()
        {
            string result = null;

            if (_httpContext?.Request != null)
                result = _httpContext.Request.UserHostAddress;

            if (result == "::1")
                result = "127.0.0.1";

            return result ?? string.Empty;
        }

        public bool IsCurrentConnectionSecured()
        {
            return _httpContext?.Request != null
                   && _httpContext.Request.IsSecureConnection;
        }
    }
}