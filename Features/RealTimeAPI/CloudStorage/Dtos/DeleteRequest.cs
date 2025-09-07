using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

public class DeleteRequest
{
    public CloudProvider cloudProvider { get; set; }
    public string objectId { get; set; }

    public DeleteRequest()
    {
    }

    public DeleteRequest(
        CloudProvider cloudProvider,
        String objectId
    )
    {
        this.cloudProvider = cloudProvider;
        this.objectId = objectId;
    }
}