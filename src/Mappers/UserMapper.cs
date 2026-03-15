using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class UserMapper : IMapperBase<User, UserDto>
{
    public partial UserDto MapToDto(User entity, Guid tenantId);
    
    public partial User MapToEntity(UserDto dto, Guid tenantId);
}