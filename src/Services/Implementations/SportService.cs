using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class SportService (ISportRepository repository)
    : ServiceBase<Sport, ISportRepository>(repository),  ISportService;