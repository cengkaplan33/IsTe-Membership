using System.Data.Entity.Core.Objects;
using System.Web;
using StackExchange.Redis;

namespace Membership.Data.Redis
{
    public class RedisFactory
    {
        private const string DbContextKey = "MEMBERSHIP_REDIS_CONTEXT";

        public static ConnectionMultiplexer DbInstance
        {
            get
            {
                if (!HttpContext.Current.Items.Contains(DbContextKey))
                    HttpContext.Current.Items.Add(DbContextKey, RedisStore.RedisInstance);
                return HttpContext.Current.Items[DbContextKey] as ConnectionMultiplexer;
            }
        }

        public static void RemoveContext()
        {
            if (HttpContext.Current == null || HttpContext.Current.Items[DbContextKey] == null) return;
            ((ObjectContext) HttpContext.Current.Items[DbContextKey]).Dispose();
            HttpContext.Current.Items.Remove(DbContextKey);
        }
    }
}