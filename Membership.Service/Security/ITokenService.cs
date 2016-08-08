using Membership.Core.Domain.Cache;
using Membership.Core.Domain.Security;
using Membership.Service.Models.Security;

namespace Membership.Service.Security
{
    public interface ITokenService
    {
        Token GenerateToken(ApiUser apiUser);

        TokenAuthenticationResult ValidateToken(string token, string ip);

        void SetToken(Token token);
    }
}