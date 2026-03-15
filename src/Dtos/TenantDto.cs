namespace sportdesk_backend.Dtos;

public sealed record TenantDto
{
    public Guid Id { get; init; }
    public string Name { get; init; } =  string.Empty;
}