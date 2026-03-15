using sportdesk_backend.Models;

namespace sportdesk_backend.Repositories.Interfaces;

public interface ITenantRepository
{
    Task<Tenant?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tenant>> GetAllAsync();
    Task<Tenant> CreateAsync(Tenant entity);
    Task<Tenant> UpdateAsync(Tenant entity);
    Task DeleteAsync(Guid id);
    Task SaveChangesAsync();
}