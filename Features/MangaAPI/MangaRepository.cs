using WebAPI_Template_Starter.Domain.Entities;
using WebAPI_Template_Starter.Domain.entities.@base;
using WebAPI_Template_Starter.Infrastructure.Persistence;
using WebAPI_Template_Starter.SharedKernel.configuration;
using WebAPI_Template_Starter.SharedKernel.persistence.data;

namespace WebAPI_Template_Starter.Features.MangaAPI;

[Repository]
public class MangaRepository : CrudRepository<Manga, string>, IMangaRepository
{
    private readonly AppDbContext _context;
    
    public MangaRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }

    public async Task<ICollection<Dictionary<String, Object>>> paginateManga(Pageable pageable)
    {
        var query = """
                    SELECT *
                    FROM Manga
                    LIMIT ?
                    """;
        return await _context.executeSqlRawAsync(query, pageable.limit, pageable.page);
    }
}
