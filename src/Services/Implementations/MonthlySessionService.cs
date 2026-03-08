using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class MonthlySessionService 
    : ServiceBase<MonthlySession, MonthlySessionRepository>, IMonthlySessionService
{
    public MonthlySessionService(MonthlySessionRepository repository)
        : base(repository)
    {
    }
}