namespace sportdesk_backend.Models;

public class MonthlySession : EntityBase
{
    public required User Coach { get; set; }
    public required Sport Sport { get; set; }
    public required decimal Fee { get; set; }
}