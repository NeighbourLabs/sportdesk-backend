using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class AthleteEnrollmentController(IAthleteEnrollmentService service, IMapperBase<AthleteEnrollment, AthleteEnrollmentDto> mapper)
    : BaseController<AthleteEnrollment, AthleteEnrollmentDto, IAthleteEnrollmentService>(service, mapper);