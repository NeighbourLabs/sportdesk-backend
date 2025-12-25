using sportdesk_backend.Enums;

namespace sportdesk_backend.Models;

public class User : EntityBase
{
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public DateOnly Birthdate { get; set; } 
    public string Tel { get; set; } = string.Empty;
    public required UserRole Role { get; set; }
}