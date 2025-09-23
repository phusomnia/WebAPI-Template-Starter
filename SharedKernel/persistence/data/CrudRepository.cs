using Microsoft.EntityFrameworkCore;
using WebAPI_Template_Starter.SharedKernel.persistence.data;

namespace WebAPI_Template_Starter.Infrastructure.Persistence;

public class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public CrudRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<ICollection<TEntity>> getAllAsync() => await _dbSet.ToListAsync();
    
    public async Task<TEntity?> findByIdAsync(TKey id) => await _dbSet.FindAsync(id);
    
    public async Task<Int32> addAsync(TEntity entity) { await _dbSet.AddAsync(entity); return await _context.SaveChangesAsync(); }

    public async Task<Int32> updateAsync(TEntity entity){ _dbSet.Update(entity); return await _context.SaveChangesAsync(); }

    public async Task<Int32> deleteAsync(TEntity entity){ _dbSet.Remove(entity); return await _context.SaveChangesAsync(); }
}