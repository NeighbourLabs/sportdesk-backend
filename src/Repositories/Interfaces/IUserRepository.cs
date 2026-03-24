using sportdesk_backend.Models;

namespace sportdesk_backend.Repositories.Interfaces;

public interface IUserRepository : IRepositoryBase<User>
{
    Task<User?> GetByEmailAsync(string email, Guid tenantId);
}