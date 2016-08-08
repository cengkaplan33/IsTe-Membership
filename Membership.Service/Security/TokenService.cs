using System.Linq;
using Membership.Core;
using Membership.Core.Domain.Cache;
using Membership.Core.Domain.Security;
using Membership.Core.Extension;
using Membership.Core.Helper;
using Membership.Data.Redis.Cache;
using Membership.Data.Repositories.Application;
using Membership.Data.Repositories.Security;
using Membership.Service.Models.Security;

namespace Membership.Service.Security
{
    public class TokenService : ITokenService
    {
        private readonly IApplicationRepository _applicationRepository;
        private readonly IApplicationCacheRepository _cacheRepository;
        private readonly ISecurityRepository _securityRepository;
        private readonly IWebHelper _webHelper;

        public TokenService(ISecurityRepository securityRepository,
            IApplicationCacheRepository cacheRepository,
            IApplicationRepository applicationRepository,
            IWebHelper webHelper)
        {
            _securityRepository = securityRepository;
            _cacheRepository = cacheRepository;
            _applicationRepository = applicationRepository;
            _webHelper = webHelper;
        }

        public Token GenerateToken(ApiUser apiUser)
        {
            var application = _applicationRepository.GetApplicationById(apiUser.ApplicationId);

            var token = new Token
            {
                ApplicationCode = application.ApplicationCode,
                TokenId = $"{application.ApplicationCode}:{TokenHelper.Encrypt(TokenHelper.GenerateToken(), apiUser.Salt)}",
                RequestIp = _webHelper.GetCurrentIpAddress()
            };

            var apiUserApiList = _securityRepository.GetApiUserIpList(apiUser);

            foreach (var item in apiUserApiList)
            {
                token.IpList.Add(item.Ip);
            }

            return token;
        }

        public TokenAuthenticationResult ValidateToken(string token, string ip)
        {
            if (token.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => token);

            if (ip.IsNullOrWhitespace())
                ExceptionHelper.ThrowIfNullOrEmpty(() => ip);

            var appCode = TokenHelper.GetApplicationCode(token);

            var applicationToken = _cacheRepository.GetApplicationToken(appCode);

            if (applicationToken == null)
                return TokenAuthenticationResult.InvalidToken;

            if (applicationToken.TokenId != token)
                return TokenAuthenticationResult.InvalidToken;

            if (applicationToken.IpList.Count == 0)
                return TokenAuthenticationResult.Successful;

            return applicationToken.IpList.Any(appIp => appIp == ip)
                ? TokenAuthenticationResult.Successful
                : TokenAuthenticationResult.InvalidIp;
        }

        public void SetToken(Token token)
        {
            _cacheRepository.SetApplicationToken(token);
        }
    }
}