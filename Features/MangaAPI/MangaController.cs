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
        var result = await _service.getManga();
        var response = new APIResponse<Object>();
        response.status = HttpStatusCode.OK.ToString();
        response.data = result;
        response.message = "Get manga";
        return Ok(response);
    }
    
    [HttpPost]
    public async Task<IActionResult> uploadMangaAPI(
        [FromBody] MangaDTO dto    
    )
    {
        var res = await _service.uploadManga(dto);
        return Ok(res);
    }
    
    [HttpPut("{id}")]
    public async Task<IActionResult> editMangaAPI(
        [FromBody] MangaDTO dto
    )
    {
        var id = RouteData.Values["id"]?.ToString();
        Console.WriteLine(id);
        var res = await _service.editManga(id, dto);
        return Ok("Manga updated");
    }
    
    [HttpDelete("{id}")]
    public async Task<ActionResult> deleteMangaAPI()
    {
        var id = RouteData.Values["id"]?.ToString();
        var result = await _service.deleteManga(id);
        return Ok();
    }
}