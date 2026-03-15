using sportdesk_backend.Dtos;
using sportdesk_backend.Mappers;
using sportdesk_backend.Models;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

public sealed class PaymentController(IPaymentService service, IMapperBase<Payment, PaymentDto> mapper) 
    : BaseController<Payment, PaymentDto, IPaymentService>(service, mapper);