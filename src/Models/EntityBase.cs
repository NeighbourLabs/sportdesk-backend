namespace sportdesk_backend.Models;

public abstract record EntityBase
{
    public Guid Id { get; set; } =  Guid.Empty;
    public DateTime CreatedAt { get; set; }
    public DateTime UpdatedAt { get; set; }
    public Tenant Tenant { get; set; } = new Tenant();
}