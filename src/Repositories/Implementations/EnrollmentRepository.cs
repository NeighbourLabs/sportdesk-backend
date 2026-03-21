using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class EnrollmentRepository(AppDbContext context)
    : RepositoryBase<Enrollment>(context), IEnrollmentRepository
{
    public override async Task<Enrollment?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.CoachSport)
            .Where(e => e.TenantId == tenantId)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<IEnumerable<Enrollment>> GetAllAsync(Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.CoachSport)
            .Where(e => e.TenantId == tenantId)
            .ToListAsync();
    }
}
