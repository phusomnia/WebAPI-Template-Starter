using System.Net;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

public class CloudinaryStorage : ICloudStorge
{
    private readonly CloudinaryConfig _config;
    private readonly CloudinaryDotNet.Cloudinary _cloudinary;

    public CloudinaryStorage(
        CloudinaryConfig config
    )
    {
        _config = config;
        _cloudinary = _config.cloudinary();
    }

    public async Task<T> isConnectedAsync<T>()
    {
        PingResult? ping = await _cloudinary.PingAsync();
        Object result = ping.StatusCode == HttpStatusCode.OK;
        return (T)result;
    }

    public async Task<T> uploadImageAsync<T>(IFormFile file)
    {
        try
        {
            await using var stream = file.OpenReadStream();
            var publicId = Guid.NewGuid() + "." + file.ContentType.Split("/")[1];
        
            var uploadParams = new ImageUploadParams
            {
                File = new FileDescription(file.FileName, stream),
                PublicId = publicId
            };
        
            Object result = await _cloudinary.UploadAsync(uploadParams);
            return (T)result;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw new Exception(e.Message);
        }
    }

    public async Task<T> editImageAsync<T>(EditRequest req)
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
        Object result = await _cloudinary.UploadAsync(editParams);
        return (T)result;
    }

    public async Task<T> deleteImageAsync<T>(string publicId)
    {
        var deletionParams = new DeletionParams(publicId);
        Object result = await _cloudinary.DestroyAsync(deletionParams);
        return (T)result;
    }
}