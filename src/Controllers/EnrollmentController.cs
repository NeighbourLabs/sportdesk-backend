using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class EnrollmentController(IEnrollmentService service, IMapperBase<Enrollment, EnrollmentDto> mapper) 
    : BaseController<Enrollment, EnrollmentDto, IEnrollmentService>(service, mapper);