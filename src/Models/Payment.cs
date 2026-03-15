namespace sportdesk_backend.Models;

public sealed record Payment : EntityBase
{
    public AthleteEnrollment AthleteEnrollment { get; init; } = new AthleteEnrollment();
    public int Months { get; init; }
}