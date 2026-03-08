namespace sportdesk_backend.Models;

public class Payment : EntityBase
{
    public required MonthlySession MonthlySession { get; set; }
    public required AthleteEnrollment AthleteEnrollment { get; set; }
    public required DateOnly ExpiresAt { get; set; }
}