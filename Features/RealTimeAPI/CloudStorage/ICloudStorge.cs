using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

public interface ICloudStorge
{
    Task<T> isConnectedAsync<T>();
    Task<T> uploadImageAsync<T>(IFormFile file);
    Task<T> editImageAsync<T>(EditRequest req);
    Task<T> deleteImageAsync<T>(String publicId);
}
