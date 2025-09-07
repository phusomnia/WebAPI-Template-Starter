using WebAPI_Template_Starter.Features.CacheAPI.Redis;

namespace WebAPI_Template_Starter.Features.CacheAPI;

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
    
    public async Task setAsync(CacheProvider cacheProvider, String key, Object value)
    {
        var instance = createInstance(cacheProvider);
        await instance.setAsync(key, value);
    }

    public async Task<TValue?> getAsync<TValue>(CacheProvider cacheProvider, String key)
    {
        var instance = createInstance(cacheProvider);
        var result = await instance.getAsync<TValue>(key);
        return result;
    }
}