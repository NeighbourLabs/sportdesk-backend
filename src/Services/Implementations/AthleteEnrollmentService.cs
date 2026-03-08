using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class AthleteEnrollmentService 
    : ServiceBase<AthleteEnrollment, IAthleteEnrollmentRepository>, IAthleteEnrollmentService
{
    public AthleteEnrollmentService(IAthleteEnrollmentRepository repository)
        : base(repository)
    {
    }
}