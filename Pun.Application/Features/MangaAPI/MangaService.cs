using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Features.MangaAPI;
using WebAPI_Template_Starter.Infrastructure.Configuration;
using WebAPI_Template_Starter.Infrastructure.CustomException;

namespace WebAPI_Template_Starter.Application.Features.MangaAPI;

[Service]
public class MangaService
{
    private readonly MangaRepository _repo;

    public MangaService(MangaRepository repo)
    {
        _repo = repo;
    }
    
    public async Task<ICollection<Manga>> getManga()
    {
        return await _repo.getAllAsync();
    }

    public async Task<Manga> uploadManga(MangaDTO req)
    {
        Manga manga = new Manga();
        manga.Id = Guid.NewGuid().ToString();
        manga.Title = req.Title;
        var affectedRows = await _repo.addAsync(manga);
        if(affectedRows < 0) throw new ApplicationException("add failed");
        return manga;
    }
    
    public async Task<Manga?> editManga(String id, MangaDTO req)
    {
        var manga = await _repo.findByIdAsync(id);
        if (manga == null) throw APIException.BadRequest("Can't find manga");
        manga.Title = req.Title;
        var affectedRows = await _repo.updateAsync(manga);
        if(affectedRows < 0) throw new ApplicationException("update failed");
        return manga;
    }
    
    public async Task<Manga> deleteManga(String id)
    {
        var manga = await _repo.findByIdAsync(id);
        if (manga == null) throw APIException.BadRequest("Can't find manga");
        var affectedRows = await _repo.deleteAsync(manga);
        if(affectedRows < 0) throw new ApplicationException("delete failed");
        return manga;
    }
}