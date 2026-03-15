namespace sportdesk_backend.Dtos;

public sealed record AthleteEnrollmentDto : DtoBase
{
    public Guid EnrollmentId { get; init; } = Guid.Empty;
    public Guid AthleteId { get; init; } = Guid.Empty;
    public int Months { get; init; }
}