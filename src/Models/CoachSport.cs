using sportdesk_backend.Enums;

namespace sportdesk_backend.Models;

public sealed record CoachSport : EntityBase
{
    public required User Coach { get; init; }
    public required Sport Sport { get; init; }
}