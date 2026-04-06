namespace sportdesk_backend.Dtos.Auth;

public sealed record InviteRequest
{
    public string Email { get; init; } = string.Empty;
}
