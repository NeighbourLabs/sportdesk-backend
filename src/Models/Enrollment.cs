namespace sportdesk_backend.Models;

public sealed record Enrollment : EntityBase
{
    public Guid CoachSportId { get; init; }
    public CoachSport CoachSport { get; init; } = null!;
    public decimal Fee { get; init; }
}