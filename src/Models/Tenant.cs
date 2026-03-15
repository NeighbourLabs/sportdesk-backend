namespace sportdesk_backend.Models;

public sealed record Tenant
{
    public Guid Id { get; set; } = Guid.Empty;
    public string Name { get; set; } =  string.Empty;
}