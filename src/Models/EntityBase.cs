namespace sportdesk_backend.Models;

public abstract record EntityBase
{
    public required Guid Id { get; set; }
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public required Tenant Tenant { get; set; }
}