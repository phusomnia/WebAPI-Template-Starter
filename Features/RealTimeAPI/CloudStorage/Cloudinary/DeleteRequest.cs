namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

public class DeleteRequest
{
    public CloudProvider provider { get; set; }
    public string publicId { get; set; }

    public DeleteRequest()
    {
    }

    public DeleteRequest(
        CloudProvider provider,
        string publicId
    )
    {
        this.provider = provider;
        this.publicId = publicId;
    }
}