namespace sportdesk_backend.Models;

public class Athlete : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Tel { get; set; } = string.Empty;
}