namespace sportdesk_backend.Models;

public sealed record Sport : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Description { get; set; } = string.Empty;
}