using System.Collections.Generic;
using Membership.Core.Domain.Security;
using Membership.Service.Models.Security;

namespace Membership.Service.Api
{
    public interface IApiAuthService
    {
        ApiKeyAuthenticationResult ValidateApiKey(string apiKey, string secretKey, string ip);

        List<ApiUserIp> GetApiUserApiList(ApiUser apiUser);

        ApiUser GetApiUserByApiKey(string apiKey, string secretKey);
    }
}