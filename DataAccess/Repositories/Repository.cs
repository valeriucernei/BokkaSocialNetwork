using System.ComponentModel.DataAnnotations;
using System.Linq.Expressions;
using AutoMapper;
using Common.Models.PagedRequest;
using DataAccess.Extensions;
using DataAccess.Interfaces;
using Domain;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Repositories;

public class Repository : IRepository
{
    private readonly Context _context;
    private readonly IMapper _mapper;

    public Repository(Context context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }
    
    public async Task<List<TEntity>> GetAll<TEntity>() where TEntity : BaseEntity
    {
        return await _context.Set<TEntity>().ToListAsync();
    }

    public async Task<TEntity> GetById<TEntity>(string id) where TEntity : BaseEntity
    {
        return (await _context.FindAsync<TEntity>(id))!;
    }

    public async Task<TEntity> GetByIdWithInclude<TEntity>
    (
        string id, 
        params Expression<Func<TEntity, object>>[] includeProperties
    ) 
        where TEntity : BaseEntity
    {
        var query = IncludeProperties(includeProperties);
        return (await query.FirstOrDefaultAsync(entity => entity.Id.ToString() == id))!;
    }

    public async void Add<TEntity>(TEntity entity) where TEntity : BaseEntity
    {
        await _context.Set<TEntity>().AddAsync(entity);
    }

    public async Task<TEntity> Delete<TEntity>(string id) where TEntity : BaseEntity
    {
        var entity = await _context.Set<TEntity>().FindAsync(id);
        
        if (entity is null)
        {
            throw new ValidationException($"Object of type {typeof(TEntity)} with id { id } not found");
        }

        _context.Set<TEntity>().Remove(entity);

        return entity;
    }

    public async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }

    public async Task<PaginatedResult<TDto>> GetPagedData<TEntity, TDto>(PagedRequest pagedRequest) 
        where TEntity : BaseEntity 
        where TDto : class
    {
        return await _context.Set<TEntity>().CreatePaginatedResultAsync<TEntity, TDto>(pagedRequest, _mapper);
    }

    private IQueryable<TEntity> IncludeProperties<TEntity>(params Expression<Func<TEntity, object>>[] includeProperties) 
        where TEntity : BaseEntity
    {
        IQueryable<TEntity> entities = _context.Set<TEntity>();
        foreach (var includeProperty in includeProperties)
        {
            entities = entities.Include(includeProperty);
        }
        return entities;
    }
}