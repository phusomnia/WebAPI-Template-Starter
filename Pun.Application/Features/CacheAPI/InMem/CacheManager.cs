using Microsoft.Extensions.Caching.Memory;

namespace WebAPI_Template_Starter.Features.CacheAPI.InMem;

public class CacheManager
{
    private readonly IMemoryCache _cache;
    
    public CacheManager(IMemoryCache cache)
    {
        _cache = cache;
    }
}