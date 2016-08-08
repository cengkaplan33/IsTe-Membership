using System;
using StackExchange.Redis;

namespace Membership.Data.Redis
{
    internal class RedisStore
    {
        private static readonly Lazy<ConnectionMultiplexer> LazyConnection;

        static RedisStore()
        {
            var configurationOptions = new ConfigurationOptions
            {
                EndPoints = {"localhost"}
            };

            LazyConnection = new Lazy<ConnectionMultiplexer>(() =>
                ConnectionMultiplexer.Connect(configurationOptions));
        }

        public static ConnectionMultiplexer RedisInstance => LazyConnection.Value;
    }
}