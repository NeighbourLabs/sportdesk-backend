namespace sportdesk_backend.Dtos;

public sealed record CoachSportDto : DtoBase
{
    public Guid CoachId { get; init; } = Guid.Empty;
    public Guid SportId { get; init; } = Guid.Empty;
}