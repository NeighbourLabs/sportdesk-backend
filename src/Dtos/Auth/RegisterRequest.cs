namespace sportdesk_backend.Dtos.Auth;

public sealed record RegisterRequest
{
    public string Token { get; init; } = string.Empty;
    public string TenantName { get; init; } = string.Empty;
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Tel { get; init; } = string.Empty;
}
