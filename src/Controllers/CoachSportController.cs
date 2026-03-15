using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class CoachSportController(ICoachSportService service, IMapperBase<CoachSport, CoachSportDto> mapper) 
    : BaseController<CoachSport, CoachSportDto, ICoachSportService>(service, mapper);