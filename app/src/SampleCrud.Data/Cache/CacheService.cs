using System.Text.Json;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using SampleCrud.Domain.Cache;
using StackExchange.Redis;

namespace SampleCrud.Data.Cache
{
    public class CacheService : ICacheService
    {
        const string CACHE_PREFIX = "SampleCrud";
        private readonly ILogger<CacheService> _logger;
        private readonly IConnectionMultiplexer _redisConnection;
        private IDatabase _cache;

        public CacheService(ILogger<CacheService> logger, IConnectionMultiplexer redisConnection)
        {
            _logger = logger;
            _redisConnection = redisConnection;
            _cache = _redisConnection.GetDatabase();
        }

        public Task<T?> Get<T>(string key)
        {
            try
            {
                _logger.LogInformation("CacheRepository.Get<{Type}>({Key})", typeof(T).Name, key);
                key = $"{CACHE_PREFIX}:{key}";
                var json = _cache.StringGet(key);
                if (json.HasValue)
                {
                    _logger.LogInformation("CacheRepository.Get<{Type}>({Key}) -> {Json}", typeof(T).Name, key, json);
                    return Task.FromResult(JsonSerializer.Deserialize<T>(json));
                }
                _logger.LogInformation("CacheRepository.Get<{Type}>({Key}) -> null", typeof(T).Name, key);
                return default;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "CacheRepository.Get<{Type}>({Key}) -> Exception: {Message}", typeof(T).Name, key, e.Message);
                throw new Exception($"Errors: {e.Message}");
            }
        }

        public Task Set<T>(string key, T value, TimeSpan? expiration = null)
        {
            try
            {
                _logger.LogInformation("CacheRepository.Set<{Type}>({Key}, {Value})", typeof(T).Name, key, value);
                key = $"{CACHE_PREFIX}:{key}";
                var json = JsonSerializer.Serialize<T>(value);
                _logger.LogInformation("CacheRepository.Set<{Type}>({Key}, {Value}) -> {Json}", typeof(T).Name, key, value, json);
                _cache.StringSet(key, json, expiry: TimeSpan.FromMinutes(15),flags: CommandFlags.FireAndForget);
                return Task.CompletedTask;
            }
            catch (Exception e)
            {
                _logger.LogError(e, "CacheRepository.Set<{Type}>({Key}, {Value}) -> Exception: {Message}", typeof(T).Name, key, value, e.Message);
                throw;
            }
        }
    }
}