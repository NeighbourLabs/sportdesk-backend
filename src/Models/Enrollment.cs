namespace sportdesk_backend.Models;

public sealed record Enrollment : EntityBase
{
    public required CoachSport CoachSport { get; init; }
    public required decimal Fee { get; init; }
}