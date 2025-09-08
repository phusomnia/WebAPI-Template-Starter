using StackExchange.Redis;
using WebAPI_Template_Starter.Features.CacheAPI.Dtos;
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

    public T isConnectedT<T>()
    {
        Object isConnected = _redis.Multiplexer.IsConnected;
        return (T)isConnected;
    }

    public async Task setAsync(SetCacheRequest req)
    {
        var json = JsonSerializer.Serialize(req.value);
        await _redis.StringSetAsync(req.key, json);
    }

    public async Task<T?> getAsync<T>(GetCacheRequest req)
    {
        var redisValue = await _redis.StringGetAsync(req.key);
        if (!redisValue.HasValue)
        {
            return default;
        }
        var result = JsonSerializer.Deserialize<T>(redisValue);
        return result;
    }
}