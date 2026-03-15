using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class CoachSportMapper : IMapperBase<CoachSport, CoachSportDto>
{
    public partial CoachSportDto MapToDto(CoachSport entity, Guid tenantId);

    public partial CoachSport MapToEntity(CoachSportDto dto, Guid tenantId);
}