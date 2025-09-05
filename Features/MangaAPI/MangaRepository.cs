using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Annotation;
using WebAPI_Template_Starter.Infrastructure.Database;

namespace WebAPI_Template_Starter.Features.MangaAPI;

[Repository]
public class MangaRepository : CrudRepository<Manga, String>
{
    public MangaRepository(AppDbContext context) : base(context)
    {
    }
}