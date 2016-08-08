using Membership.Core.Domain.Cache;

namespace Membership.Service.Cache
{
    public interface ICacheService
    {
        void SetAccountCache(AccountCache accountCache);
    }
}