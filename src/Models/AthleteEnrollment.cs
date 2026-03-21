namespace sportdesk_backend.Models;

public sealed record AthleteEnrollment : EntityBase
{
    public Guid EnrollmentId { get; init; }
    public Enrollment Enrollment { get; init; } = null!;
    public Guid AthleteId { get; set; }
    public Athlete Athlete { get; set; } = null!;
    public int Months { get; init; }
}