using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

[Service]
public class CloudStorageService
{
    public ICloudStorge createInstance(CloudProvider provider)
    {
        switch (provider)
        {
            case CloudProvider.Cloudinary:
                return new CloudinaryStorage(new CloudinaryConfig());
            default:
                throw new ArgumentException("Unknown cloud storage name as " + provider);
        }
    }
    
    public async Task<T> isConnectedAsync<T>(CloudProvider provider)
    {
        var instance = createInstance(provider);
        return await instance.isConnectedAsync<T>();
    }

    public async Task<T> uploadImage<T>(UploadRequest req)
    {
        if (req.file.Count == 0) throw new Exception("No files uploaded."); // need to test this once
        if(!req.file[0].ContentType.Contains("image")) throw new Exception("File is not image");
        
        var instance = createInstance(req.cloudProvider);
        
        return await instance.uploadImageAsync<T>(req.file[0]);
    }
    
    public async Task<T> editImage<T>(EditRequest req)
    {
        if (req.file.Count == 0) throw new Exception("No files uploaded.");
        if(!req.file[0].ContentType.Contains("image")) throw new Exception("File is not image");
        
        var instance = createInstance(req.cloudProvider);
        
        return await instance.editImageAsync<T>(req);
    }

    public async Task<T> deleteImage<T>(DeleteRequest req)
    {
        var instance = createInstance(req.cloudProvider);
        return await instance.deleteImageAsync<T>(req.objectId);
    }
}