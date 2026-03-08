using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class SportService 
    : ServiceBase<Sport, SportRepository>,  ISportService
{
    public SportService(SportRepository repository)
        : base(repository)
    {
    }
}