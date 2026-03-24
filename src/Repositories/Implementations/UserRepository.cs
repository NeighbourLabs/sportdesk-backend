using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class UserRepository(AppDbContext context)
    : RepositoryBase<User>(context), IUserRepository
{
    public async Task<User?> GetByEmailAsync(string email, Guid tenantId)
    {
        return await _dbSet.FirstOrDefaultAsync(u => u.Email == email && u.TenantId == tenantId);
    }
}
