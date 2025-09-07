using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

public interface ICloudStorge
{
    Object uploadIamge(IFormFile file);
    Object editImage(EditRequest req);
    Object deleteImage(String publicId);
}