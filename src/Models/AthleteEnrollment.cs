namespace sportdesk_backend.Models;

public sealed record AthleteEnrollment : EntityBase
{
    public Enrollment Enrollment { get; init; } = new Enrollment();
    public Athlete Athlete { get; set; } = new Athlete();
    public int Months { get; init; }
}