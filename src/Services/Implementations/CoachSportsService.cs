using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class CoachSportsService 
    : ServiceBase<CoachSport, CoachSportsRepository>, ICoachSportsService
{
    public CoachSportsService(CoachSportsRepository repository)
        : base(repository)
    {
    }
}