using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

[Configuration]
public class CloudinaryConfig
{
    private readonly IConfiguration _config;

    public CloudinaryConfig(IConfiguration config)
    {
        _config = config;
    }

    public CloudinaryDotNet.Cloudinary cloudinary()
    {
        var config = _config["Cloudinary:url"];
        CloudinaryDotNet.Cloudinary cloud = new CloudinaryDotNet.Cloudinary(config);
        cloud.Api.Secure = true;
        return cloud;
    }
}