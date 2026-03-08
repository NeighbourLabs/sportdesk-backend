namespace sportdesk_backend.Models;

public sealed record Payment : EntityBase
{
    public required AthleteEnrollment AthleteEnrollment { get; init; }
    public required int Months { get; init; }
}