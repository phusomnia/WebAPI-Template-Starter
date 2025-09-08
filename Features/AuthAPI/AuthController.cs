using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.AccountAPI;
using WebAPI_Template_Starter.Features.AuthAPI.Dtos;
using WebAPI_Template_Starter.Infrastructure.Pub_Sub;
using WebAPI_Template_Starter.Infrastructure.Security.Authorization;

namespace WebAPI_Template_Starter.Features.AuthAPI;

[ApiController]
[Route("/api/v1/auth/")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;
    private readonly IPublisher  _publisher;
    
    public AuthController(
        AuthService authService,
        IPublisher  publisher
    )
    {
        _authService = authService;
        _publisher   = publisher;
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
    
    [HttpPost("publisher")]
    public async Task<IActionResult> publisherAPI(
        [FromQuery] String queueName,
        [FromQuery] String message
    )
    {
        try
        {
            await _publisher.publishMessageAsync(queueName, message);
            return Accepted("Send message queued :D");
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