namespace WebAPI_Template_Starter.Features.AuthAPI.Dtos;

public class EmailRequest
{
    public string to { get; set; }
    public string subject { get; set; }
    public string body { get; set; }

    public EmailRequest(string to, string subject, string body)
    {
        this.to = to;
        this.subject = subject;
        this.body = body;
    }
}