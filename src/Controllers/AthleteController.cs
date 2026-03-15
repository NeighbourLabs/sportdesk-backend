using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class AthleteController(IAthleteService service, IMapperBase<Athlete, AthleteDto> mapper) 
    : BaseController<Athlete, AthleteDto, IAthleteService>(service, mapper);