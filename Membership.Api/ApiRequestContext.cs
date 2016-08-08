using System;
using System.Web;
using Membership.Core;
using Membership.Core.Domain.Application;
using Membership.Core.Extension;
using Membership.Service.Application;

namespace Membership.Api
{
    public class ApiRequestContext : IRequestContext
    {
        private readonly IApplicationService _applicationService;
        private readonly HttpContextBase _httpContext;
        private readonly IWebHelper _webHelper;

        public ApiRequestContext(HttpContextBase httpContext,
            IWebHelper webHelper,
            IApplicationService applicationService)
        {
            _httpContext = httpContext;
            _webHelper = webHelper;
            _applicationService = applicationService;

            InitializeRequest();
        }

        public Application CurrentApplication { get; set; }
        public string RequestIpAddress { get; set; }
        public string UserAgent { get; set; }
        public string RequestUrl { get; set; }
        public DateTime RequestTime { get; set; }

        /// <summary>
        ///     Request Header bilgisinden token değerini döndürür.
        /// </summary>
        public string GetTokenFromRequestHeader()
        {
            var header = _httpContext.Request.Headers;

            if (header["TOKEN"].IsNullOrWhitespace())
                return string.Empty;

            var token = header["TOKEN"];

            return GetApplicationCode(token);
        }

        private static string GetApplicationCode(string token)
        {
            try
            {
                if (token.IsNullOrWhitespace())
                    return string.Empty;

                var value = token.Split(':');

                return value[0];
            }
            catch
            {
                return string.Empty;
            }
        }

        private void InitializeRequest()
        {
            RequestIpAddress = _webHelper.GetCurrentIpAddress();
            UserAgent = _httpContext.Request.UserAgent;
            RequestUrl = _httpContext.Request.Url?.AbsoluteUri;
            RequestTime = DateTime.Now;

            var token = GetTokenFromRequestHeader();

            if (token.IsNullOrWhitespace()) return;

            var applicationCode = GetApplicationCode(token);

            if (applicationCode.IsNullOrWhitespace()) return;

            var application = _applicationService.GetAApplicationByCode(applicationCode);

            CurrentApplication = application;
        }
    }
}