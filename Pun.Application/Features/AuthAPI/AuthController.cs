using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Application.AccountAPI;
using WebAPI_Template_Starter.Application.Features.AccountAPI;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.AuthAPI.Dtos;
using WebAPI_Template_Starter.Infrastructure.Security.Attribute;
using WebAPI_Template_Starter.Shared;

namespace WebAPI_Template_Starter.Features.AuthAPI;

[ApiController]
[Route("/api/v1/")]
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
        var result = await _authService.register(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Login successfully",
            result
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost("login")]
    public async Task<IActionResult> loginAccountAPI(
        [FromBody] AccountDTO req
    )
    {
        var result = await _authService.authenticate(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Login successfully",
            result
        );

        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost("forget-password")]
    public async Task<IActionResult> forgetPasswordAPI(
        String email
    )
    {
        await _authService.generateOTP(email);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Send otp successfully"
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    [HttpPost("verify-otp")]
    public async Task<IActionResult> verifyOtpAPI(
        VerifyOtpRequest req
    )
    {
        await _authService.verifyOtp(req);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "verified successfully"
        );
        
        return StatusCode(response.statusCode, response);
    }
    
    // [HttpPost("publisher")]
    // public async Task<IActionResult> publisherAPI(
    //     [FromQuery] String queueName,
    //     [FromQuery] String message
    // )
    // {
    //     try
    //     {
    //         await _publisher.publishMessageAsync(queueName, message);
    //         return Accepted("Send message queued :D");
    //     }
    //     catch (Exception e)
    //     {
    //         return Problem(
    //             detail: e.Message,
    //             statusCode: 500
    //         );
    //     }
    // }
    
    [HttpGet("auth/{url}")]
    [PreAuthorize("hasRole(#roleAllowed)")]
    public ActionResult AdminDashboard(
        [FromQuery] String roleAllowed, 
        String url)
    {
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            $"Welcome {url}",
            url
        );

        return StatusCode(response.statusCode, response);
    }
    
    // [HttpGet("auth/secure-by-permission")]
    // [Permission("Export")]
    // public ActionResult<Object> exportPDF()
    // {
    //     return "PDF is ok";
    // }
}