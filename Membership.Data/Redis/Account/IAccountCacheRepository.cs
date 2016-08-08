using Membership.Core.Domain.Cache;

namespace Membership.Data.Redis.Account
{
    public interface IAccountCacheRepository
    {
        bool IsAccountCacheValid(string accountCode);

        void SetAccountCache(AccountCache accountCache);

        AccountCache GetAccountCache(string accountCode);
    }
}