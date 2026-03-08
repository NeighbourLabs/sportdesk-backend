using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Implementations;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class PaymentService 
    : ServiceBase<Payment, PaymentRepository>,  IPaymentService
{
    public PaymentService(PaymentRepository repository)
        : base(repository)
    {
    }
}