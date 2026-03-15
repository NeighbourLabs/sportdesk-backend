using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class AthleteEnrollmentMapper : IMapperBase<AthleteEnrollment, AthleteEnrollmentDto>
{
    public partial AthleteEnrollmentDto MapToDto(AthleteEnrollment entity, Guid tenantId);

    public partial AthleteEnrollment MapToEntity(AthleteEnrollmentDto dto, Guid tenantId);
}