using System.Net;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Domain.Enums;
using WebAPI_Template_Starter.Features.CacheAPI;
using WebAPI_Template_Starter.Features.CacheAPI.Dtos;
using WebAPI_Template_Starter.Features.CacheAPI.Redis;
using WebAPI_Template_Starter.SharedKernel.configuration;
using WebAPI_Template_Starter.SharedKernel.utils;

namespace WebAPI_Template_Starter.Application.Features.CacheAPI;

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

    public APIResponse<Object> isConnected(CacheProvider cacheProvider)
    {
        var instance = createInstance(cacheProvider);
        var result = instance.isConnected();
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Connect is ok",
            result
        );

        return response;
    }

    public async Task<APIResponse<Object>> setAsync(SetCacheRequest req)
    {
        var instance = createInstance(req.cacheProvider);
        await instance.setAsync(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Set value successfully"
        );

        return response;
    }

    public async Task<APIResponse<Object>> getAsync<TValue>(GetCacheRequest req)
    {
        var instance = createInstance(req.cacheProvider);
        var result = await instance.getAsync<TValue>(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Set value successfully",
            result!
        );

        return response;
    }
}