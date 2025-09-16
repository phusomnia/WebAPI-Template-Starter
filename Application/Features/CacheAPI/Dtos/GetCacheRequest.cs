using System.ComponentModel;
using WebAPI_Template_Starter.Domain.Enums;

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