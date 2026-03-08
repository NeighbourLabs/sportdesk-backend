namespace sportdesk_backend.Models;

public class AthleteEnrollment : EntityBase
{
    public required CoachSport CoachSport { get; set; }
    public required Athlete Athlete { get; set; }
}