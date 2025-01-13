using EventManagement_BusinessObjects;
using EventManagement_BusinessObjects.Common;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq.Expressions;
using System.Threading.Tasks;

public class BaseDAO<TEntity> where TEntity : BaseEntity
{
    private readonly EventManagementDbContext _context;
    private readonly DbSet<TEntity> _dbSet;

    public BaseDAO(EventManagementDbContext context)
    {
        _context = context ?? throw new ArgumentNullException(nameof(context));
        _dbSet = context.Set<TEntity>();
    }

    /// <summary>
    /// Checks if an entity of type TEntity exists in the database with the specified field.
    /// </summary>
    /// <param name="filter">The filter predicate of the entity.</param>
    /// <returns>A Task<bool> indicating whether the entity exists.</returns>
    public Task<bool> ExistsAsync(Expression<Func<TEntity, bool>> filter)
    {
        return _dbSet.AnyAsync(filter);
    }

    /// <summary>
    /// Retrieves all entities of type TEntity from the database.
    /// </summary>
    /// <returns>A Task containing an array of TEntity.</returns>
    public virtual Task<List<TEntity>> GetAllAsync()
    {
        return _dbSet.ToListAsync();
    }

    public virtual async Task<IEnumerable<TEntity>> GetAllByParametersAsync(Expression<Func<TEntity, bool>>? filter = null)
    {
        if (filter != null)
        {
            return await _dbSet.Where(filter).ToListAsync();
        }

        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity> GetByParametersAsync(Expression<Func<TEntity, bool>> filter)
    {
        return await _dbSet.FirstOrDefaultAsync(filter) ?? default;
    }

    /// <summary>
    /// Retrieves an entity of type TEntity from the database with the specified id.
    /// </summary>
    /// <param name="id">The ID of the entity.</param>
    /// <returns>A Task containing the entity, or null if not found.</returns>
    public virtual Task<TEntity?> GetAsync(Guid id)
    {
        return _dbSet.FindAsync(id).AsTask();
    }

    /// <summary>
    /// Adds a new record of type TEntity to the database.
    /// </summary>
    /// <param name="entity">The entity to add.</param>
    /// <returns>A Task containing the added entity.</returns>
    public async Task<TEntity> AddAsync(TEntity entity)
    {
        if (entity == null) throw new ArgumentNullException(nameof(entity));

        _dbSet.Add(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    /// <summary>
    /// Saves changes made to the database.
    /// </summary>
    /// <returns>A Task containing the number of affected rows.</returns>
    public Task<int> SaveChangesAsync()
    {
        return _context.SaveChangesAsync();
    }

    /// <summary>
    /// Deletes an entity of type TEntity from the database with the specified id.
    /// </summary>
    /// <param name="id">The ID of the entity to delete.</param>
    /// <returns>A Task containing the number of affected rows, or 0 if not found.</returns>
    public async Task<int> DeleteAsync(Guid id)
    {
        var entity = await GetAsync(id);
        if (entity == null) return 0;

        _dbSet.Remove(entity);
        return await _context.SaveChangesAsync();
    }
}
