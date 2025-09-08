namespace WebAPI_Template_Starter.Features.AuthAPI.Dtos;

public class VerifyOtpRequest
{
    public string email { get; set; }
    public string otptCode { get; set; }
    
    
    public VerifyOtpRequest(string email, string otptCode)
    {
        this.email = email;
        this.otptCode = otptCode;
    }
}