namespace WebAPI_Template_Starter.Domain.Core.BaseModel;

public class ErrorResponse
{
    public int statusCode { get; set; }
    public Object message { get; set; }
    public DateTime timeStamp { get; set; }

    public ErrorResponse(int statusCode, Object message, DateTime timeStamp)
    {
        this.statusCode = statusCode;
        this.message = message;
        this.timeStamp = timeStamp;
    }
}
