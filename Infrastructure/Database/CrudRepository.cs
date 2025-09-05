using Microsoft.EntityFrameworkCore;

namespace WebAPI_Template_Starter.Infrastructure.Database;

public class CrudRepository<TEntity, TKey> : ICrudRepository<TEntity, TKey> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public CrudRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = context.Set<TEntity>();
    }

    public async Task<ICollection<Dictionary<String, Object>>> executeSqlRawAsync(String query, params Object[] parameters)
    {
        return await _context.executeSqlRawAsync(query, parameters);
    }
    
    public async Task<ICollection<TEntity>> getAllAsync()
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity?> findByIdAsync(TKey id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<Int32> addAsync(TEntity entity)
    {
        await _dbSet.AddAsync(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<Int32> updateAsync(TEntity entity)
    {
        _dbSet.Update(entity);
        return await _context.SaveChangesAsync();
    }

    public async Task<Int32> deleteAsync(TEntity entity)
    {
        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}