using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class TenantService : ITenantService
{
    private readonly TenantRepository Repository;

    public TenantService(TenantRepository repository)
    {
        Repository = repository;
    }

    public virtual async Task<Tenant?> GetByIdAsync(Guid id)
    {
        return await Repository.GetByIdAsync(id);
    }

    public virtual async Task<IEnumerable<Tenant>> GetAllAsync()
    {
        return await Repository.GetAllAsync();
    }

    public virtual async Task<Tenant> CreateAsync(Tenant entity)
    {
        return await Repository.CreateAsync(entity);
    }

    public virtual async Task<Tenant> UpdateAsync(Tenant entity)
    {
        return await Repository.UpdateAsync(entity);
    }

    public virtual async Task DeleteAsync(Guid id)
    {
        await Repository.DeleteAsync(id);
    }
}
