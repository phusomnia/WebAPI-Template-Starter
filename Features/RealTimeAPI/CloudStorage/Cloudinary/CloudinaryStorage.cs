using CloudinaryDotNet;
using CloudinaryDotNet.Actions;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

public class CloudinaryStorage : ICloudStorge
{
    private readonly CloudinaryConfig _config;
    private readonly CloudinaryDotNet.Cloudinary _cloundinary;

    public CloudinaryStorage(
        CloudinaryConfig config
    )
    {
        _config = config;
        _cloundinary = _config.cloudinary();
    }
    
    public Object uploadIamge(IFormFile file)
    {
        var stream = file.OpenReadStream();
        var customFileName = Guid.NewGuid() + "." + file.ContentType.Split("/")[1];
        
        var uploadParams = new ImageUploadParams()
        {
            File = new FileDescription(file.FileName, stream),
            PublicId = customFileName
        };
        var uploadResult = _cloundinary.Upload(uploadParams);
        
        return uploadResult;
    }

    public Object editImage(EditRequest req)
    {
        var editParams = new ImageUploadParams()
        {
            File = new FileDescription(req.file[0].FileName, req.file[0].OpenReadStream()),
            PublicId = req.publicId,
            /*
             * w_300,h_300
             */
            Transformation = String.IsNullOrEmpty(req.transformation) ? null : new Transformation().RawTransformation(req.transformation),
            Overwrite = true,
            Invalidate = true
        };
        var editResult = _cloundinary.Upload(editParams);
        
        return editResult;
    }

    public Object deleteImage(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        var deletionResult = _cloundinary.Destroy(deletionParams);
        return deletionResult;
    }
}