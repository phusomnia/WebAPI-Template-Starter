namespace WebAPI_Template_Starter.Infrastructure.Database;

public interface ICrudRepository<TEntity, TKey> where TEntity : class
{
    public Task<ICollection<TEntity>> getAllAsync();
    public Task<TEntity?> findByIdAsync(TKey id);
    public Task<Int32> addAsync(TEntity entity);
    public Task<Int32> updateAsync(TEntity entity);
    public Task<Int32> deleteAsync(TEntity entity);
}