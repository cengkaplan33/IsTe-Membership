using Membership.Core.Domain.Cache;
using Membership.Data.Redis.Account;

namespace Membership.Service.Cache
{
    public class CacheService : ICacheService
    {
        private readonly IAccountCacheRepository _accountCacheRepository;        

        public CacheService(IAccountCacheRepository accountCacheRepository)
        {
            _accountCacheRepository = accountCacheRepository;            
        }

        public void SetAccountCache(AccountCache accountCache)
        {
            _accountCacheRepository.SetAccountCache(accountCache);
        }
    }
}