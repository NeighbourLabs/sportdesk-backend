using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;

namespace sportdesk_backend.Repositories.Implementations;

public class AthleteEnrollmentRepository : RepositoryBase<AthleteEnrollment>, IAthleteEnrollmentRepository
{
    public AthleteEnrollmentRepository(AppDbContext context) : base(context)
    {
    }
}