using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class TenantMapper
{
    public partial TenantDto MapToDto(Tenant entity);
    
    public partial Tenant MapToEntity(TenantDto dto);
}