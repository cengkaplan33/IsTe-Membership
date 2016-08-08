using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using Membership.Core;
using Membership.Service.Api;
using Membership.Service.Models.Security;
using Membership.Service.Resources;
using Membership.Service.Security;

namespace Membership.Api.Attribute
{
    /// <summary>
    ///     Request Header içindeki Token bilgisini kontrol eder,
    ///     eğer bulumazsa HttpStatusCode.Forbidden (Http 403) döner.
    /// </summary>
    public class TokenValidationAttribute : ActionFilterAttribute
    {
        private const string RequestHeaderToken = "TOKEN";
        private ITokenService _tokenService;
        private IWebHelper _webHelper;

        public override void OnActionExecuting(HttpActionContext actionContext)
        {
            var request = actionContext.Request;

            if (request.Headers.Contains(RequestHeaderToken))
            {
                var token = request.Headers.GetValues(RequestHeaderToken).First();

                _tokenService = (ITokenService) request.GetDependencyScope().GetService(typeof (ITokenService));

                _webHelper = (IWebHelper) request.GetDependencyScope().GetService(typeof (IWebHelper));

                var requestIp = _webHelper.GetCurrentIpAddress();

                var result = _tokenService.ValidateToken(token, requestIp);

                if (result == TokenAuthenticationResult.InvalidToken)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                        ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                            ServiceResponse.INVALID_TOKEN, ServiceResponseMessage.INVALID_TOKEN));

                if (result == TokenAuthenticationResult.InvalidIp)
                    actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                        ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                            ServiceResponse.INVALID_IP_ADDRESS, ServiceResponseMessage.INVALID_IP_ADDRESS));
            }
            else
                actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Forbidden,
                    ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                        ServiceResponse.INVALID_REQUEST_HEADER_TOKEN));
        }
    }
}