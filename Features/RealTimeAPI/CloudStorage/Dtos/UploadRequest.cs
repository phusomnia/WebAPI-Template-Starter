using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

public class UploadRequest
{
    public CloudProvider cloudProvider { get; set; }
    public List<IFormFile> file { get; set; }

    public UploadRequest()
    {
    }

    public UploadRequest(
        CloudProvider cloudProvider,
        List<IFormFile> file
    )
    {
        this.cloudProvider = cloudProvider;
        this.file = file;
    }
}