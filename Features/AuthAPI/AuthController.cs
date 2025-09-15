using System.Net;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.AccountAPI;
using WebAPI_Template_Starter.Features.AuthAPI.Dtos;
using WebAPI_Template_Starter.Infrastructure.Security.Authorization;

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
    
    [HttpPost("forget-password")]
    public async Task<IActionResult> forgetPasswordAPI(
        String email
    )
    {
        try
        {
            var result = await _authService.generateOTP(email);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.ToString(),
                "Send otp successfully",
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
    
    [HttpPost("verify-otp")]
    public async Task<IActionResult> verifyOtpAPI(
        VerifyOtpRequest req
    )
    {
        try
        {
            await _authService.verifyOtp(req);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.ToString(),
                "verified successfully"
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
    
    
    [HttpGet("auth/{role}")]
    [PreAuthorize("hasRole(#role)")]
    public ActionResult<String> AdminDashboard(String role, String url)
    {
        return Ok(new { url = url });
    }
    
    // [HttpGet("auth/secure-by-permission")]
    // [Permission("Export")]
    // public ActionResult<Object> exportPDF()
    // {
    //     return "PDF is ok";
    // }
}