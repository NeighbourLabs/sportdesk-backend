using sportdesk_backend.Enums;

namespace sportdesk_backend.Models;

public sealed record CoachSport : EntityBase
{
    public User Coach { get; init; } = new User();
    public Sport Sport { get; init; } = new Sport();
}