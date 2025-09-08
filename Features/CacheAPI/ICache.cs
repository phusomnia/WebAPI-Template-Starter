using WebAPI_Template_Starter.Features.CacheAPI.Dtos;

namespace WebAPI_Template_Starter.Features.CacheAPI;

public interface ICache
{
    public T isConnectedT<T>();
    public Task setAsync(SetCacheRequest req);
    public Task<T?> getAsync<T>(GetCacheRequest req);
}