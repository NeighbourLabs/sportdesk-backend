using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class UserController(IUserService service, IMapperBase<User, UserDto> mapper) 
    : BaseController<User, UserDto, IUserService>(service, mapper);