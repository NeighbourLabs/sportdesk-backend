namespace sportdesk_backend.Models;

public class Payment : EntityBase
{
    public required Session Session { get; set; }
    public required AthleteEnrollments AthleteEnrollment { get; set; }
    public required DateOnly ExpiresAt { get; set; }
}