using System.Net;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using WebAPI_Template_Starter.Application.Features.MangaAPI;
using WebAPI_Template_Starter.Domain.Core.BaseModel;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.SharedKernel.utils;

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
    [AllowAnonymous]
    public async Task<IActionResult> getMangaAPI()
    {
        var result = await _service.getManga();
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get manga",
            result
        );
        
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> uploadMangaAPI(
        [FromBody] MangaDTO dto    
    )
    {
        var result = await _service.uploadManga(dto);
        
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get manga",
            result
        );
        
        return Ok(response);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> editMangaAPI(
        [FromBody] MangaDTO dto
    )
    {
        var id = RouteData.Values["id"]?.ToString();
        var result = await _service.editManga(id, dto);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get manga",
            result
        );
        
        return Ok(response);
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteMangaAPI()
    {
        var id = RouteData.Values["id"]?.ToString();
        var result = await _service.deleteManga(id);
        var response = new APIResponse<Object>(
            HttpStatusCode.OK.value(),
            "Get manga",
            result
        );
        
        return Ok(response);
    }
}