using Newtonsoft.Json;
using StackExchange.Redis;
using WebAPI_Template_Starter.Infrastructure.Utils;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace WebAPI_Template_Starter.Features.CacheAPI.Redis;

[Service]
public class NRedisCache : ICache
{
    private IDatabase _redis;

    public NRedisCache(NRedisConfig config)
    {
        _redis = config.redis();
    }

    public void set(string key, object value)
    {
        RedisValue redisValue = JsonSerializer.Serialize(value);
        _redis.StringSet(key, redisValue);
    }

    public TValue get<TValue>(string key)
    {
        RedisValue value = _redis.StringGet(key);
        Console.WriteLine($"Value: {value.ToString()}");
        
        if (value.IsNullOrEmpty)
        {
            throw new KeyNotFoundException($"The key '{key}' was not found");
        }

        return JsonSerializer.Deserialize<TValue>(value);
    }

    public async Task setAsync(string key, object value)
    {
        var json = JsonSerializer.Serialize(value);
        await _redis.StringSetAsync(key, json);
    }

    public async Task<TValue?> getAsync<TValue>(string key)
    {
        var redisValue = await _redis.StringGetAsync(key);
        if (!redisValue.HasValue)
        {
            return default;
        }
        var result = JsonSerializer.Deserialize<TValue>(redisValue)!;
        return result;
    }
}