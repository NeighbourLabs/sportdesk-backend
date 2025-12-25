using sportdesk_backend.Enums;

namespace sportdesk_backend.Models;

public class CoachSport : EntityBase
{
    public required User Coach { get; init; }
    public required Sport Sport { get; init; }

    public CoachSport(User coach, Sport sport)
    {
        if (coach == null || coach.Role != UserRole.COACH)
            throw new ArgumentException("Coach must not be null and be of role type Coach");
        Sport = sport;
    }
}