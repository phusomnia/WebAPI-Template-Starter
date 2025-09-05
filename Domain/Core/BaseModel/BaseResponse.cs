namespace WebAPI_Template_Starter.Domain.Core.BaseModel;

public class BaseResponse
{
    public String status { get; set; }
    public String message { get; set; }

    public BaseResponse() { }

    public BaseResponse(String status,String message)
    {
        this.status = status;
        this.message = message;
    }
}