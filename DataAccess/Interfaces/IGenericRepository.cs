namespace DataAccess.Interfaces;

public interface IGenericRepository<TEntity> : IDisposable
{
    IEnumerable<TEntity> GetEntityList();
    
    Task<TEntity> GetEntity(long id);
    
    Task Create(TEntity entity);
    
    Task Update(TEntity entity);
    
    Task Delete(TEntity entity);
    
    Task Save();
}