using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class AthleteService 
    : ServiceBase<Athlete, IAthleteRepository>, IAthleteService
{
    public AthleteService(IAthleteRepository repository)
        : base(repository)
    {
    }
}