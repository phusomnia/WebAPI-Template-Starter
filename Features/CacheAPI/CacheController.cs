using System.ComponentModel;
using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.CacheAPI.Dtos;

namespace WebAPI_Template_Starter.Features.CacheAPI;

[ApiController]
[Route("/api/v1/cache/")]
public class CacheController : ControllerBase
{
    private readonly CacheService _service;

    public CacheController(
        CacheService service
        )
    {
        _service = service;
    }

    [HttpPost("check-connection")]
    public ActionResult checkConnection(
        CacheProvider cacheProvider
        )
    {
        try
        {
            var result = _service.isConnected<Boolean>(cacheProvider);
            
            var response = new APIResponse<Object>(
                status  : HttpStatusCode.OK.ToString(),
                message : "Connect is ok",
                data    : result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
    }
    
    [HttpGet()]
    public async Task<ActionResult> getValue([FromQuery] GetCacheRequest req)
    {
        try
        {
            var result = await _service.getAsync<Object>(req);
            
            var response = new APIResponse<Object>(
                status  : HttpStatusCode.OK.ToString(),
                message : "Connect is ok",
                data: result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
    }
    
    [HttpPost()]
    public async Task<ActionResult> setValue(
        Dtos.SetCacheRequest req
        )
    {
        try
        {
            await _service.setAsync(req);
            
            var response = new APIResponse<Object>(
                status  : HttpStatusCode.OK.ToString(),
                message : "Connect is ok",
                data: req
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
    }

}