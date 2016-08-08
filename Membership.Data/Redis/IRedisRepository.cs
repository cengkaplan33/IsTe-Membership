namespace Membership.Data.Redis
{
    public interface IRedisRepository<T>
    {
        T GetObject<TType>(string key);

        string GetString(string key);

        void SaveObject(string key, T obj);

        void SaveString(string key, string value);

        void Delete(string key);
    }
}