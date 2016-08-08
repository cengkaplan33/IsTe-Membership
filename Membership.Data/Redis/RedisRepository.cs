using Newtonsoft.Json;
using StackExchange.Redis;

namespace Membership.Data.Redis
{
    public class RedisRepository<T> : RedisFactory, IRedisRepository<T>
    {
        private readonly IDatabase _redisStore;

        public RedisRepository()
        {
            _redisStore = DbInstance.GetDatabase();
        }

        public T GetObject<TType>(string key)
        {
            var value = JsonConvert.DeserializeObject<T>(_redisStore.StringGet(key));

            return value;
        }

        public string GetString(string key)
        {
            return _redisStore.StringGet(key);
        }

        public void SaveObject(string key, T obj)
        {
            var serializedObject = JsonConvert.SerializeObject(obj);
            
            _redisStore.StringSet(key, serializedObject);
        }

        public void SaveString(string key, string value)
        {
            if (_redisStore.StringSet(key, value))
                _redisStore.StringGet(key);
        }

        public void Delete(string key)
        {
            _redisStore.KeyDelete(key);
        }
    }
}