namespace sportdesk_backend.Models;

public sealed record Tenant
{
    public Guid Id { get; set; }
    public Guid Name { get; set; }
}