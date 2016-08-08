using System.Collections.Generic;
using System.Linq;
using Membership.Core.Domain.Security;
using Membership.Core.Enum;
using Membership.Core.Extension;
using Membership.Core.Helper;
using Membership.Data.Repositories.Application;
using Membership.Data.Repositories.Security;
using Membership.Service.Models.Security;

namespace Membership.Service.Api
{
    public class ApiAuthService : IApiAuthService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly ISecurityRepository _securityRepository;

        public ApiAuthService(ISecurityRepository securityRepository,
            IApplicationRepository applicationRepository)
        {
            _securityRepository = securityRepository;
            _applicationRepository = applicationRepository;
        }

        public ApiKeyAuthenticationResult ValidateApiKey(string apiKey, string secretKey, string ip)
        {
            if (apiKey.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => apiKey);

            if (secretKey.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => secretKey);

            if (ip.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => ip);

            var apiUser = _securityRepository.GetApiUserByApiKey(apiKey, secretKey);

            if (apiUser == null)
                return ApiKeyAuthenticationResult.ApiKeyNotExist;

            if (apiUser.Status == (byte) GeneralEnum.Status.Passive)
                return ApiKeyAuthenticationResult.NotActive;

            var application = _applicationRepository.GetApplicationById(apiUser.ApplicationId);

            if (application == null)
                return ApiKeyAuthenticationResult.ApiKeyNotExist;

            if (application.Status == (byte) GeneralEnum.Status.Passive)
                return ApiKeyAuthenticationResult.NotActive;

            var apiUserApiList = _securityRepository.GetApiUserIpList(apiUser);

            if (apiUserApiList.Count == 0)
                return ApiKeyAuthenticationResult.Successful;

            return apiUserApiList.Any(apUseriIp => apUseriIp.Ip == ip)
                ? ApiKeyAuthenticationResult.Successful
                : ApiKeyAuthenticationResult.InvalidIp;
        }

        public List<ApiUserIp> GetApiUserApiList(ApiUser apiUser)
        {
            if (apiUser == null)
                ExceptionHelper.ThrowIfNull(() => apiUser);

            var apiUserApiList = _securityRepository.GetApiUserIpList(apiUser);

            return apiUserApiList;
        }

        public ApiUser GetApiUserByApiKey(string apiKey, string secretKey)
        {
            return _securityRepository.GetApiUserByApiKey(apiKey, secretKey);
        }
    }
}