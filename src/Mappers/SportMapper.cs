using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class SportMapper : IMapperBase<Sport, SportDto>
{
    public partial SportDto MapToDto(Sport entity, Guid tenantId);
    
    public partial Sport MapToEntity(SportDto dto, Guid tenantId);
}