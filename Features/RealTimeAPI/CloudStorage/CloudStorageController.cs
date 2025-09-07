using System.Net;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;
using WebAPI_Template_Starter.Infrastructure.Utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

[ApiController]
[Route("api/v1/image/")]
public class CloudStorageController : ControllerBase
{
    private readonly CloudinaryConfig _config;
    private readonly CloudStorageService _service;
    
    public CloudStorageController(
        CloudinaryConfig config,
        CloudStorageService service
    )
    {
        _config  = config;
        _service = service;
    }
    
    [HttpGet("check-connection")]
    public async Task<IActionResult> checkConnection()
    {
        try
        {
            var cloudinary = _config.cloudinary();
            PingResult? result = await cloudinary.PingAsync();

            if (result.StatusCode != HttpStatusCode.OK) throw new Exception("Fack this");
            
            Console.WriteLine();
            
            return Ok("Connected to cloud");
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "Error", message = e.Message });
        }
    }
    
    [HttpPost]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> uploadImageAPI(
        UploadRequest req
    )
    {
        try
        {
            var id = RouteData.Values["objectId"]!.ToString();
            Console.WriteLine(id);
            
            var result = await _service.uploadImage(req);
            var response = new APIResponse<Object>(
                status  : HttpStatusCode.OK.ToString(),
                message : "Upload successfully",
                data    : result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "Error", message = e.Message });
        }
    }
    
    [HttpPut]
    [Consumes("multipart/form-data")]
    public async Task<IActionResult> editImageAPI(EditRequest req)
    {
        try
        {
            var result = await _service.editImage(req);
            var response = new APIResponse<Object>(
                status  : HttpStatusCode.OK.ToString(),
                message : "Edit successfully",
                data    : result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "Error", message = e.Message });
        }
    }
    
    [HttpDelete]
    public ActionResult deleteImageAPI([FromQuery] DeleteRequest req)
    {
        try
        {
            Console.WriteLine(CustomJson.json(req, CustomJsonOptions.WriteIndented));
            // var result = _service.delete(req);
            return Ok();
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}