using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class TenantRepository :  RepositoryBase<Tenant>, ITenantRepository
{
    public TenantRepository(AppDbContext context) : base(context)
    {
    }
}