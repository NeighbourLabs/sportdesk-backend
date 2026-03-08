using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class SportRepository : RepositoryBase<Sport>, ISportRepository
{
    public SportRepository(AppDbContext context) : base(context)
    {
    }
}