using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class EnrollmentMapper : IMapperBase<Enrollment, EnrollmentDto>
{
    public partial EnrollmentDto MapToDto(Enrollment enrollment, Guid tenantId);

    public partial Enrollment MapToEntity(EnrollmentDto dto, Guid tenantId);
}