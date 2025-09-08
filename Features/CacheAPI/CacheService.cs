using WebAPI_Template_Starter.Features.CacheAPI.Dtos;
using WebAPI_Template_Starter.Features.CacheAPI.Redis;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.CacheAPI;

[Service]
public class CacheService
{    
    public ICache createInstance(CacheProvider provider)
    {
        switch (provider)
        {
            case CacheProvider.Redis:
                return new NRedisCache(new NRedisConfig());    
            default:
                throw new ArgumentException("Unknown cache name as " + provider);
        }
    }

    public T isConnected<T>(CacheProvider cacheProvider)
    {
        var instance = createInstance(cacheProvider);
        return instance.isConnectedT<T>();
    }

    public async Task setAsync(SetCacheRequest req)
    {
        var instance = createInstance(req.cacheProvider);
        await instance.setAsync(req);
    }

    public async Task<TValue?> getAsync<TValue>(GetCacheRequest req)
    {
        var instance = createInstance(req.cacheProvider);
        var result = await instance.getAsync<TValue>(req);
        return result;
    }
}