using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class AthleteService(IAthleteRepository repository) 
    : ServiceBase<Athlete, IAthleteRepository>(repository), IAthleteService;
