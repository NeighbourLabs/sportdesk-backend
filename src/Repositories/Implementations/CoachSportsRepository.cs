using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class CoachSportsRepository(AppDbContext context)
    : RepositoryBase<CoachSport>(context), ICoachSportsRepository
{
    public override async Task<CoachSport?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.Coach)
            .Include(e => e.Sport)
            .Where(e => e.TenantId == tenantId)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<IEnumerable<CoachSport>> GetAllAsync(Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.Coach)
            .Include(e => e.Sport)
            .Where(e => e.TenantId == tenantId)
            .ToListAsync();
    }
}
