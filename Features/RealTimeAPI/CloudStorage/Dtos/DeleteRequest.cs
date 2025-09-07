using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

public class DeleteRequest
{
    public CloudProvider provider { get; set; }
    public string objectId { get; set; }

    public DeleteRequest()
    {
    }

    public DeleteRequest(
        CloudProvider provider,
        String objectId
    )
    {
        this.provider = provider;
        this.objectId = objectId;
    }
}