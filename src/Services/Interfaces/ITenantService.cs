using sportdesk_backend.Models;

namespace sportdesk_backend.Services.Interfaces;

public interface ITenantService
{
    Task<Tenant?> GetByIdAsync(Guid id);
    Task<IEnumerable<Tenant>> GetAllAsync();
    Task<Tenant> CreateAsync(Tenant entity);
    Task<Tenant> UpdateAsync(Tenant entity);
    Task DeleteAsync(Guid id);
}