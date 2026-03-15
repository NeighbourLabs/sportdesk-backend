namespace sportdesk_backend.Dtos;

public sealed record EnrollmentDto : DtoBase
{
    public Guid CoachSportId { get; init; } = Guid.Empty;
    public decimal Fee { get; init; }
}