using WebAPI_Template_Starter.Features.CacheAPI.Dtos;

namespace WebAPI_Template_Starter.Application.Features.CacheAPI;

public interface ICache
{
    public Boolean isConnected();
    public Task setAsync(SetCacheRequest req);
    public Task<T?> getAsync<T>(GetCacheRequest req);
}