using Membership.Core.Domain.Cache;
using Membership.Core.Extension;
using Newtonsoft.Json;

namespace Membership.Data.Redis.Cache
{
    public class ApplicationCacheRepository : IApplicationCacheRepository
    {
        private readonly IRedisRepository<Token> _applicationCacheRepository;

        public ApplicationCacheRepository(IRedisRepository<Token> applicationCacheRepository)
        {
            _applicationCacheRepository = applicationCacheRepository;
        }

        public bool IsApplicationTokenValid(string applicationCode)
        {
            var session = _applicationCacheRepository.GetString($"{CacheKeyName.Application}:{applicationCode}");

            return !session.IsNullOrWhitespace();
        }

        public void SetApplicationToken(Token token)
        {
            var key = $"{CacheKeyName.Application}:{token.ApplicationCode}";

            _applicationCacheRepository.SaveObject(key, token);
        }

        public Token GetApplicationToken(string applicationCode)
        {
            var session = _applicationCacheRepository.GetString($"{CacheKeyName.Application}:{applicationCode}");

            return !session.IsNullOrWhitespace() 
                ? JsonConvert.DeserializeObject<Token>(session) 
                : null;
        }      
    }
}