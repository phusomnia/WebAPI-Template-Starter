using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Infrastructure.Persistence;
using WebAPI_Template_Starter.SharedKernel.configuration;
using WebAPI_Template_Starter.SharedKernel.persistence.data;

namespace WebAPI_Template_Starter.Application.Features.MangaAPI;

[Repository]
public class MangaRepository : CrudRepository<Manga, String>
{
    public MangaRepository(AppDbContext context) : base(context)
    {
    }
}
