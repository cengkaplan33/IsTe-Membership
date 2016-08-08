using System.Linq;
using System.Web.Http;
using Membership.Api.Models.Token.Response;
using Membership.Core;
using Membership.Service.Api;
using Membership.Service.Models.Security;
using Membership.Service.Resources;
using Membership.Service.Security;

namespace Membership.Api.Controllers
{
    public class ApplicationController : ApiController
    {
        private readonly IApiAuthService _apiAuthServiceService;
        private readonly ITokenService _tokenService;
        private readonly IWebHelper _webHelper;

        public ApplicationController(IApiAuthService apiAuthServiceService,
            ITokenService tokenService, IWebHelper webHelper)
        {
            _apiAuthServiceService = apiAuthServiceService;
            _tokenService = tokenService;
            _webHelper = webHelper;
        }

        [HttpPost]
        [Route("token")]
        public IHttpActionResult Token()
        {
            const string requestHeaderApiKey = "X-MEMBERSHIP-APIKEY";
            const string requestHeaderSecretKey = "SECRET-KEY";

            if (!Request.Headers.Contains(requestHeaderApiKey) || !Request.Headers.Contains(requestHeaderSecretKey))
                return Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_INPUT_ERROR,
                    ServiceResponse.INVALID_REQUEST_HEADER));

            var apiKey = Request.Headers.GetValues(requestHeaderApiKey).First();
            var secretKey = Request.Headers.GetValues(requestHeaderSecretKey).First();

            var authResult = _apiAuthServiceService.ValidateApiKey(apiKey, secretKey, _webHelper.GetCurrentIpAddress());

            switch (authResult)
            {
                case ApiKeyAuthenticationResult.ApiKeyNotExist:
                    return
                        Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                            ServiceResponse.INVALID_API_KEY, ServiceResponseMessage.INVALID_API_KEY));
                case ApiKeyAuthenticationResult.NotActive:
                    return
                        Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                            ServiceResponse.API_ACCOUNT_DISABLED, ServiceResponseMessage.ACCOUNT_DISABLE));
                case ApiKeyAuthenticationResult.InvalidIp:
                    return
                        Ok(ApiHelper.BuildBaseResponse(ServiceResponse.SERVICE_STATUS_FAILED,
                            ServiceResponse.INVALID_IP_ADDRESS, ServiceResponseMessage.INVALID_IP_ADDRESS));
            }

            var apiUser = _apiAuthServiceService.GetApiUserByApiKey(apiKey, secretKey);

            var token = _tokenService.GenerateToken(apiUser);

            var tokenResponse = new TokenResponseModel
            {
                Token = token.TokenId,
                Status = ServiceResponse.SERVICE_STATUS_SUCCESS
            };

            _tokenService.SetToken(token);

            return Ok(tokenResponse);
        }
    }
}