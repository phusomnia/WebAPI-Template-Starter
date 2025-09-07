using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Features.CacheAPI.Redis;

namespace WebAPI_Template_Starter.Features.CacheAPI;

[ApiController]
[Route("/api/v1/cache/")]
public class CacheController : ControllerBase
{
    private readonly NRedisConfig _config;

    public CacheController(
        NRedisConfig config
        )
    {
        _config = config;
    }

    [HttpPost("check-connection")]
    public async Task<IActionResult> checkConnection()
    {
        try
        {
            var isConnected = _config.redis().Multiplexer.IsConnected;
            if (!isConnected) throw new Exception("Can't connect to redis");
            return Ok();
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