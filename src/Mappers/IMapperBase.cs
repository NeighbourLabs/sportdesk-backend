using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

public interface IMapperBase<E, D> where E : EntityBase where D : DtoBase
{
    D MapToDto(E entity, Guid tenantId);
    E MapToEntity(D dto, Guid tenantId);
}