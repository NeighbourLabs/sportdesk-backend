using sportdesk_backend.Infra;
using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Models;

namespace sportdesk_backend.Repositories;

public abstract class RepositoryBase<T>(AppDbContext context) 
    : IRepositoryBase<T> where T : EntityBase
{
    protected readonly AppDbContext _context = context;
    protected readonly DbSet<T> _dbSet = context.Set<T>();

    public virtual async Task<T?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _dbSet
            .Where(e => e.Tenant.Id == tenantId)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public virtual async Task<IEnumerable<T>> GetAllAsync(Guid tenantId)
    {
        return await _dbSet
            .Where(e => e.Tenant.Id == tenantId)
            .ToListAsync();
    }

    public virtual async Task<T> CreateAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<T> UpdateAsync(T entity, Guid tenantId)
    {
        if (entity.Tenant.Id != tenantId)
        {
            throw new UnauthorizedAccessException();
        }

        _dbSet.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(Guid id, Guid tenantId)
    {
        var entity = await GetByIdAsync(id, tenantId);
        if (entity != null)
        {
            _dbSet.Remove(entity);
            await SaveChangesAsync();
        }
    }

    public virtual async Task SaveChangesAsync()
    {
        await _context.SaveChangesAsync();
    }
}   