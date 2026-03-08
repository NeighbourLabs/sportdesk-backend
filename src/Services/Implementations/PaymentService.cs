using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class PaymentService (IPaymentRepository repository)
    : ServiceBase<Payment, IPaymentRepository>(repository),  IPaymentService;