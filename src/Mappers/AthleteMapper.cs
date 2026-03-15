using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class AthleteMapper : IMapperBase<Athlete, AthleteDto>
{
    public partial AthleteDto MapToDto(Athlete entity, Guid tenantId);

    public partial Athlete MapToEntity(AthleteDto dto, Guid tenantId);
}