namespace sportdesk_backend.Dtos.Auth;

public sealed record ResetPasswordRequest
{
    public string Token { get; init; } = string.Empty;
    public string NewPassword { get; init; } = string.Empty;
}
