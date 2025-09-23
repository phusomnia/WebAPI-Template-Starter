using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;

public class EditRequest
{
    [DefaultValue(CloudProvider.Aws)]
    public CloudProvider cloudProvider { get; set; }
    
    [Required(ErrorMessage = "public id is required")]
    public string publicId { get; set; }
    
    public string transformation { get; set; }
    
    public List<IFormFile> file { get; set; }
}