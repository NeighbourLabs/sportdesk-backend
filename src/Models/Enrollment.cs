namespace sportdesk_backend.Models;

public sealed record Enrollment : EntityBase
{
    public CoachSport CoachSport { get; init; } = new CoachSport();
    public decimal Fee { get; init; } 
}