using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

[Service]
public class CloudStorageService
{
    private readonly CloudStorageFactory _cloudStorageFactory;

    public CloudStorageService(CloudStorageFactory cloudStorageFactory)
    {
        _cloudStorageFactory = cloudStorageFactory;
    }

    public async Task<Object> uploadImage(UploadRequest req)
    {
        Console.WriteLine(CustomJson.json(req, CustomJsonOptions.WriteIndented));
        if (req.file.Count == 0) throw new Exception("No files uploaded."); // need to test this once
        if(!req.file[0].ContentType.Contains("image")) throw new Exception("File is not image");
        
        var instance = _cloudStorageFactory.createService(req.cloudProvider);
        return instance.uploadIamge(req.file[0]);
    }
    
    public async Task<Object> editImage(EditRequest req)
    {
        Console.WriteLine(CustomJson.json(req, CustomJsonOptions.WriteIndented));
        if (req.file.Count == 0) throw new Exception("No files uploaded.");
        if(!req.file[0].ContentType.Contains("image")) throw new Exception("File is not image");
        
        var instance = _cloudStorageFactory.createService(req.cloudProvider);
        return instance.editImage(req);
    }
}