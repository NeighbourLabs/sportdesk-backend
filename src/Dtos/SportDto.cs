namespace sportdesk_backend.Dtos;

public sealed record SportDto :  DtoBase
{
    public string Name { get; init; } = string.Empty;
    public string Description { get; init; } =  string.Empty;
}