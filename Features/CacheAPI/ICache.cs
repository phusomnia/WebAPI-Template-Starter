namespace WebAPI_Template_Starter.Features.CacheAPI;

public interface ICache
{
    public void set(String key, Object value);
    public TValue get<TValue>(String key);
    public Task setAsync(String key, Object value);
    public Task<TValue?> getAsync<TValue>(String key);
}