namespace sportdesk_backend.Models;

public sealed record Payment : EntityBase
{
    public Guid AthleteEnrollmentId { get; init; }
    public AthleteEnrollment AthleteEnrollment { get; init; } = null!;
    public int Months { get; init; }
}