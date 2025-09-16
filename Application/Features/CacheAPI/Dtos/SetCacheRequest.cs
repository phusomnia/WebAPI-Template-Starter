using System.ComponentModel;
using WebAPI_Template_Starter.Domain.Enums;

namespace WebAPI_Template_Starter.Features.CacheAPI.Dtos;

public class SetCacheRequest
{
    [DefaultValue(CacheProvider.Redis)] 
    public CacheProvider cacheProvider { get; set; }
    public string key { get; set; }
    public object value { get; set; }
    public TimeSpan ttl { get; set; }

    public SetCacheRequest(string key, object value)
    {
        this.cacheProvider = CacheProvider.Redis;
        this.key = key;
        this.value = value;
        this.ttl = TimeSpan.FromMilliseconds(600000);
    }
}