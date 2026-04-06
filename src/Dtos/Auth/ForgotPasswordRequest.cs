namespace sportdesk_backend.Dtos.Auth;

public sealed record ForgotPasswordRequest
{
    public string Email { get; init; } = string.Empty;
    public Guid TenantId { get; init; }
}
