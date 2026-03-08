using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class TenantRepository (AppDbContext context)
    : RepositoryBase<Tenant>(context), ITenantRepository;