namespace sportdesk_backend.Models;

public sealed record AthleteEnrollment : EntityBase
{
    public required Enrollment Enrollment { get; init; }
    public required Athlete Athlete { get; set; }
    public required int Months { get; init; }
}