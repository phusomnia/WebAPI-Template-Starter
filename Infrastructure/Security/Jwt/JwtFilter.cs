using System.Net;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Infrastructure.Annotation;

namespace WebAPI_Template_Starter.Infrastructure.Security.Jwt;

[Service]
public class JwtFilter : IAsyncAuthorizationFilter
{
    private readonly JwtTokenProvider _provider;
    private readonly ILogger<JwtFilter> _logger;
    
    public JwtFilter(
        JwtTokenProvider provider,
        ILogger<JwtFilter> logger
        )
    {
        _provider = provider;
        _logger = logger;
    }

    public Task OnAuthorizationAsync(AuthorizationFilterContext ctx)
    {
        var request = ctx.HttpContext.Request;
        
        var authHeader = request.Headers["Authorization"].FirstOrDefault();
        if (string.IsNullOrEmpty(authHeader) || !authHeader.StartsWith("Bearer "))
        {
            _logger.LogWarning("Unauthorized request: Missing or invalid Authorization header. Path={Path}", request.Path);
            
            ctx.Result = new ObjectResult(new APIResponse<object>(
                status: HttpStatusCode.Unauthorized.ToString(),
                message: "Missing or invalid Authorization header"
            ));
            return Task.CompletedTask;
        }

        var token = authHeader.Substring("Bearer ".Length).Trim();
        
        var principal = _provider.validateToken(token);
        if (principal == null)
        {
            _logger.LogWarning("Unauthorized request: Invalid/expired token. Path={Path}, Token={Token}", request.Path, token);
            
            ctx.Result = new ObjectResult(new APIResponse<object>(
                status: HttpStatusCode.Unauthorized.ToString(),
                message: "Invalid or expired token"
            ));
            return Task.CompletedTask;
        }
        
        ctx.HttpContext.User = principal;

        return Task.CompletedTask;
    }
}