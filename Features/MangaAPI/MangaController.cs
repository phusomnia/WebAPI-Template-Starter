using System.Net;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Domain.Entities;

namespace WebAPI_Template_Starter.Features.MangaAPI;

[ApiController]
[Route("api/v1/manga/")]
public class MangaController : ControllerBase
{
    private readonly MangaService _service;

    public MangaController(MangaService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> getMangaAPI()
    {
        try
        {
            var result = await _service.getManga();
            var response = new APIResponse<Object>(
                status: HttpStatusCode.OK.ToString(),
                message: "Get manga",
                data: result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
    }
    
    [HttpPost]
    public async Task<IActionResult> uploadMangaAPI(
        [FromBody] MangaDTO dto    
    )
    {
        try
        {
            var result = await _service.uploadManga(dto);
            var response = new APIResponse<Object>(
                status: HttpStatusCode.OK.ToString(),
                message: "Get manga",
                data: result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> editMangaAPI(
        [FromBody] MangaDTO dto
    )
    {
        try
        {
            var id = RouteData.Values["id"]?.ToString();
            var result = await _service.editManga(id, dto);
            var response = new APIResponse<Object>(
                status: HttpStatusCode.OK.ToString(),
                message: "Get manga",
                data: result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
        
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteMangaAPI()
    {
        try
        {
            var id = RouteData.Values["id"]?.ToString();
            var result = await _service.deleteManga(id);
            var response = new APIResponse<Object>(
                status: HttpStatusCode.OK.ToString(),
                message: "Get manga",
                data: result
            );
            
            return Ok(response);
        }
        catch (Exception e)
        {
            return Problem(
                detail: e.Message,
                statusCode: 500
            );
        }
        
    }
}