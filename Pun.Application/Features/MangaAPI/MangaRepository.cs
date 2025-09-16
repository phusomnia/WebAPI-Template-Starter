using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Configuration;
using WebAPI_Template_Starter.Infrastructure.Persistence;

namespace WebAPI_Template_Starter.Application.Features.MangaAPI;

[Repository]
public class MangaRepository : CrudRepository<Manga, String>
{
    public MangaRepository(AppDbContext context) : base(context)
    {
    }
}
