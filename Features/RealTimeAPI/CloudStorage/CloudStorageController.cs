using System.ComponentModel;
using System.Net;
using CloudinaryDotNet.Actions;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.entities.@base;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Cloudinary;
using WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage.Dtos;
using WebAPI_Template_Starter.SharedKernel.utils;

namespace WebAPI_Template_Starter.Features.RealTimeAPI.CloudStorage;

[ApiController]
[Route("api/v1/image/")]
public class CloudStorageController : ControllerBase
{
    private readonly CloudStorageService _service;
    
    public CloudStorageController(
        CloudinaryConfig config,
        CloudStorageService service
    )
    {
        _service = service;
    }
    
    [HttpGet("check-connection")]
    public async Task<IActionResult> checkConnection(
        CloudProvider cloudProvider
        )
    {
            var isConnected = await _service.isConnectedAsync<Boolean>(cloudProvider);
            if (!isConnected) throw new Exception("Can't connection");
            
            var response = new APIResponse<Boolean>(
                HttpStatusCode.OK.value(),
                "Edit successfully",
                isConnected
            );
            
            return StatusCode(response.statusCode, response);
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
            
            var result = await _service.uploadImage<Object>(req);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Upload successfully",
                result
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
            var result = await _service.editImage<Object>(req);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Edit successfully",
                result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return StatusCode(500, new { status = "Error", message = e.Message });
        }
    }
    
    [HttpDelete]
    public async Task<IActionResult> deleteImageAPI([FromQuery] DeleteRequest req)
    {
        try
        {
            var result = await _service.deleteImage<DeletionResult>(req);
            
            var response = new APIResponse<Object>(
                HttpStatusCode.OK.value(),
                "Delete successfully",
                result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return BadRequest(e.Message);
        }
    }
}