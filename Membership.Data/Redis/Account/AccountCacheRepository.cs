using Membership.Core.Domain.Cache;
using Membership.Core.Extension;
using Newtonsoft.Json;

namespace Membership.Data.Redis.Account
{
    public class AccountCacheRepository : IAccountCacheRepository
    {
        private readonly IRedisRepository<AccountCache> _accountCacheRepository;

        public AccountCacheRepository(IRedisRepository<AccountCache> accountCacheRepository)
        {
            _accountCacheRepository = accountCacheRepository;
        }

        public bool IsAccountCacheValid(string accountCode)
        {
            var session = _accountCacheRepository.GetString($"{CacheKeyName.Account}:{accountCode}");

            return !session.IsNullOrWhitespace();
        }

        public void SetAccountCache(AccountCache accountCache)
        {
            var key = $"{CacheKeyName.Account}:{accountCache.AccountCode}";

            _accountCacheRepository.SaveObject(key, accountCache);
        }

        public AccountCache GetAccountCache(string accountCode)
        {
            var session = _accountCacheRepository.GetString($"{CacheKeyName.Account}:{accountCode}");

            return !session.IsNullOrWhitespace()
                ? JsonConvert.DeserializeObject<AccountCache>(session)
                : null;
        }
    }
}