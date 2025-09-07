using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

[Component]
public class CloudStorageFactory
{
    private readonly CloudinaryConfig _cloudinaryConfig;

    public CloudStorageFactory(
        CloudinaryConfig cloudinaryConfig
    )
    {
        _cloudinaryConfig = cloudinaryConfig;
    }

    public ICloudStorge createService(CloudProvider provider)
    {
        switch (provider)
        {
            case CloudProvider.Cloudinary:
                return new CloudinaryStorage(_cloudinaryConfig);
            default:
                throw new ArgumentException("Unknown cloud storage name as " + provider);
        }
    }
}