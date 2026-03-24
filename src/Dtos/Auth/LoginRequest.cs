namespace sportdesk_backend.Dtos.Auth;

public sealed record LoginRequest
{
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
}
