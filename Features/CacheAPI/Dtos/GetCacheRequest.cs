using System.ComponentModel;

namespace WebAPI_Template_Starter.Features.CacheAPI.Dtos;

public class GetCacheRequest
{
    public CacheProvider cacheProvider { get; set; }
    public string key { get; set; }

    public GetCacheRequest(string key)
    {
        cacheProvider = CacheProvider.Redis;
        this.key = key;
    }
}