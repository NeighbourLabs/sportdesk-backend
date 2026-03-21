using sportdesk_backend.Models;

namespace sportdesk_backend.Dtos;

public abstract record DtoBase
{
    public Guid Id { get; init; }
    public Guid TenantId { get; init; }
}