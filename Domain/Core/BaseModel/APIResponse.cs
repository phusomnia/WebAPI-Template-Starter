using System.Text.Json.Serialization;

namespace WebAPI_Template_Starter.Domain.Core.BaseModel;

public class APIResponse<T> : BaseResponse
{
    [JsonPropertyName("data")] 
    public T data { get; set; }
    [JsonPropertyName("metadata")]
    public Dictionary<String, Object?> metadata { get; set; }
    
    public APIResponse() { }

    public APIResponse(int statusCode, string message) : base(statusCode, message)
    {
    }

    public APIResponse(int statusCode, string message, T data) : this(statusCode, message, data, null)
    {
    }

    public APIResponse(
        int statusCode, 
        String message, 
        T data, 
        Dictionary<String, Object?> metadata
    ) : base(statusCode, message)
    {
        this.data = data;
        this.metadata = metadata;
    }
}