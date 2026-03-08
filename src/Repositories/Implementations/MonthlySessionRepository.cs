using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class MonthlySessionRepository : RepositoryBase<MonthlySession>, IMonthlySessionRepository
{
    public MonthlySessionRepository(AppDbContext context) : base(context)
    {
    }
}