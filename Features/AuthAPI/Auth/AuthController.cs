using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.AccountAPI;
using WebAPI_Template_Starter.Infrastructure.Security;

namespace WebAPI_Template_Starter.Features.AuthAPI.Auth;

[ApiController]
[Route("/api/v1/auth/")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    
    public AuthController(
        AuthService authService
    )
    {
        _authService = authService;
    }
    
    [HttpPost("register")]
    public async Task<IActionResult> registerAccountAPI(
        [FromBody] AccountDTO req
    )
    {
        try
        {
            var result = await _authService.register(req);
        
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.ToString(),
                "Register successfully",
                result
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
    
    [HttpPost("login")]
    public async Task<IActionResult> loginAccountAPI(
        [FromBody] AccountDTO req
    )
    {
        try
        {

            var result = await _authService.login(req);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.ToString(),
                "Login successfully",
                result
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
    
    [HttpGet("auth/secure-by-role")]
    [Authorize(Roles = "Admin")]
    public ActionResult<String> AdminDashboard()
    {
        return Ok("hi admin");
        // return Redirect("http://localhost:3000/api/v1/auth/secure-by-permission");
    }
    
    [HttpGet("auth/secure-by-permission")]
    [Permission("Export")]
    public ActionResult<Object> exportPDF()
    {
        return "PDF is ok";
    }
}