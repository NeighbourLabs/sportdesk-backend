using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class SportController(ISportService service, IMapperBase<Sport, SportDto> mapper) 
    : BaseController<Sport, SportDto, ISportService>(service, mapper);