using sportdesk_backend.Enums;

namespace sportdesk_backend.Dtos;

public sealed record UserDto : DtoBase
{
    public string Name { get; init; } = string.Empty;
    public string Surname { get; init; } = string.Empty;
    public string Email { get; init; } = string.Empty;
    public string Password { get; init; } = string.Empty;
    public string Tel { get; init; } = string.Empty;
    public required UserRole Role { get; init; }
}