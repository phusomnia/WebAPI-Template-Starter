using Microsoft.EntityFrameworkCore.Storage;
using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Domain.entities.@base;
using WebAPI_Template_Starter.Infrastructure.Persistence;

namespace WebAPI_Template_Starter.Features.MangaAPI;

public interface IMangaRepository : ICrudRepository<Manga, String>
{
    public Task<ICollection<Dictionary<String, Object>>> paginateManga(Pageable pageable);
}