namespace sportdesk_backend.Dtos;

public sealed record AthleteDto : DtoBase
{
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Tel { get; init; } = string.Empty;
}