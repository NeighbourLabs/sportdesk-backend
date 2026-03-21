using Microsoft.EntityFrameworkCore;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class PaymentRepository(AppDbContext context)
    : RepositoryBase<Payment>(context), IPaymentRepository
{
    public override async Task<Payment?> GetByIdAsync(Guid id, Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.AthleteEnrollment)
            .Where(e => e.TenantId == tenantId)
            .FirstOrDefaultAsync(e => e.Id == id);
    }

    public override async Task<IEnumerable<Payment>> GetAllAsync(Guid tenantId)
    {
        return await _dbSet
            .Include(e => e.Tenant)
            .Include(e => e.AthleteEnrollment)
            .Where(e => e.TenantId == tenantId)
            .ToListAsync();
    }
}
