using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.entities.@base;
using WebAPI_Template_Starter.Domain.Enums;
using WebAPI_Template_Starter.Features.CacheAPI.Dtos;
using WebAPI_Template_Starter.SharedKernel.utils;

namespace WebAPI_Template_Starter.Application.Features.CacheAPI;

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
        var response = _service.isConnected(cacheProvider);

        return StatusCode(response.statusCode, response);
    }
    
    [HttpGet()]
    public async Task<ActionResult> getValue([FromQuery] GetCacheRequest req)
    {
        var response = await _service.getAsync<Object>(req);
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost()]
    public async Task<ActionResult> setValue(
        WebAPI_Template_Starter.Features.CacheAPI.Dtos.SetCacheRequest req
        )
    {
        await _service.setAsync(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Set value successfully",
            data: req
        );
        
        return StatusCode(response.statusCode, response);
    }

}