using System;
using System.IO;
using Microsoft.Extensions.Configuration;
using StackExchange.Redis;

namespace Vik_WeatherService.Redis
{
    public interface IRedisConnectionProvider
    {
        ConnectionMultiplexer Connection();
    }
    public class RedisConnectionProvider : IRedisConnectionProvider
    {
        private readonly Lazy<ConnectionMultiplexer> _redisConnection;
        public RedisConnectionProvider()
        {
            var configuration = new ConfigurationBuilder()
              .SetBasePath(Directory.GetCurrentDirectory())
              .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
             .AddEnvironmentVariables().Build();
            var connectionString = configuration["REDIS_CONNECTIONSTRING"];
            if (connectionString == null)
            {
                throw new Exception($"Missing Connection string for Redis: {connectionString}");
            }

            var options = ConfigurationOptions.Parse(connectionString);

            options.AbortOnConnectFail = false;

            _redisConnection = new Lazy<ConnectionMultiplexer>(() => ConnectionMultiplexer.Connect(options));
        }
        public ConnectionMultiplexer Connection() => _redisConnection.Value;
    }
}