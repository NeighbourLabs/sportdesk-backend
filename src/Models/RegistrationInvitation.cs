namespace sportdesk_backend.Models;

public sealed record RegistrationInvitation
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Token { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public DateTime ExpiresAt { get; set; }
    public bool IsUsed { get; set; } = false;
    public DateTime CreatedAt { get; set; }
}
