using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class TenantRepository : ITenantRepository
{
    private readonly AppDbContext _context;
    private readonly DbSet<Tenant> _dbSet;

    public TenantRepository(AppDbContext context)
    {
        _context = context;
        _dbSet = _context.Set<Tenant>();
    }

    public virtual async Task<Tenant?> GetByIdAsync(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public virtual async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<Tenant> CreateAsync(Tenant entity)
    {
        await _dbSet.AddAsync(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task<Tenant> UpdateAsync(Tenant entity)
    {
        _dbSet.Update(entity);
        await SaveChangesAsync();
        return entity;
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        var entity = await GetByIdAsync(id);
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