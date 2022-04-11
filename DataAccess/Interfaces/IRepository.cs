using System.Linq.Expressions;
using Common.Models.PagedRequest;
using Domain;

namespace DataAccess.Interfaces;

public interface IRepository
{
    Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity;
    
    Task<TEntity> GetById<TEntity>(string id) where TEntity : BaseEntity;

    Task<TEntity> GetByIdWithInclude<TEntity>(string id, params Expression<Func<TEntity, object>>[] includeProperties) 
        where TEntity : BaseEntity;

    void Add<TEntity>(TEntity entity) where TEntity : BaseEntity;

    Task<TEntity> Delete<TEntity>(string id) where TEntity : BaseEntity;
    
    Task SaveChangesAsync();

    Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) 
        where TEntity : BaseEntity
        where TDto : class;
}