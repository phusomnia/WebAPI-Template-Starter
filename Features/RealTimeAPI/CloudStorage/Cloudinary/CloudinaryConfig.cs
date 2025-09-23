using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.SharedKernel.configuration;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

[Configuration]
public class CloudinaryConfig
{
    public CloudinaryDotNet.Cloudinary cloudinary()
    {
        var _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true)
            .Build();
        
        var config = _config["Cloudinary:url"];
        CloudinaryDotNet.Cloudinary cloud = new CloudinaryDotNet.Cloudinary(config);
        cloud.Api.Secure = true;
        return cloud;
    }
}