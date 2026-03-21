using sportdesk_backend.Enums;

namespace sportdesk_backend.Models;

public sealed record CoachSport : EntityBase
{
    public Guid CoachId { get; init; }
    public User Coach { get; init; } = null!;
    public Guid SportId { get; init; }
    public Sport Sport { get; init; } = null!;
}