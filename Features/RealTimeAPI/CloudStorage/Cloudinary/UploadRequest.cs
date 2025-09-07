namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

public class UploadRequest
{
    public CloudProvider provider { get; set; }
    public List<IFormFile> file { get; set; }

    public UploadRequest()
    {
    }

    public UploadRequest(
        CloudProvider provider,
        List<IFormFile> file
    )
    {
        this.provider = provider;
        this.file = file;
    }
}