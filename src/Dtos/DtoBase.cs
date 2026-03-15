using sportdesk_backend.Models;

namespace sportdesk_backend.Dtos;

public abstract record DtoBase
{
    public required Guid Id { get; init; }
    public required Guid TenantId { get; init; }
}