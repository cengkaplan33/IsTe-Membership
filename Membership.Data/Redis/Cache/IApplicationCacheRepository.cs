using Membership.Core.Domain.Cache;

namespace Membership.Data.Redis.Cache
{
    public interface IApplicationCacheRepository
    {
        bool IsApplicationTokenValid(string applicationCode);

        void SetApplicationToken(Token token);

        Token GetApplicationToken(string applicationCode);
    }
}