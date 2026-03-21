using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class AthleteEnrollmentRepository(AppDbContext context)
    : RepositoryBase<AthleteEnrollment>(context), IAthleteEnrollmentRepository
{
    public override async Task<AthleteEnrollment?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.Enrollment)
            .Include(e => e.Athlete)
            .Where(e => e.TenantId == tenantId)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<IEnumerable<AthleteEnrollment>> GetAllAsync(Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.Enrollment)
            .Include(e => e.Athlete)
            .Where(e => e.TenantId == tenantId)
            .ToListAsync();
    }
}
