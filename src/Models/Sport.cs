namespace sportdesk_backend.Models;

public class Sport : EntityBase
{
    public required string Name { get; set; }
    public required string Description { get; set; }
}