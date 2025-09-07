using System.Text.Json.Serialization;

namespace WebAPI_Template_Starter.Domain.Core.BaseModel;

public class APIResponse<T> : BaseResponse
{
    [JsonPropertyName("data")] 
    public T data { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<String, Object> metadata { get; set; }
    
    public APIResponse(){}
    
    public APIResponse(String status,String message) : base(status, message)
    {
    }
    
    public APIResponse(String status,String message,T data, Dictionary<String, Object> metata = null) : base(status, message)
    {
        this.data = data;
        this.metadata = metata;
    }
}