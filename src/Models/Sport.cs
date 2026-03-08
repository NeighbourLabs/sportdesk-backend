namespace sportdesk_backend.Models;

public sealed record Sport : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}