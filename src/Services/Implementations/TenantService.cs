using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class TenantService 
    : ServiceBase<Tenant, TenantRepository>, ITenantService
{
    public TenantService(TenantRepository repository)
        : base(repository)
    {
    }
}
