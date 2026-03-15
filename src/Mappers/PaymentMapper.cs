using Riok.Mapperly.Abstractions;
using sportdesk_backend.Dtos;
using sportdesk_backend.Models;

namespace sportdesk_backend.Mappers;

[Mapper]
public partial class PaymentMapper : IMapperBase<Payment, PaymentDto>
{
    public partial PaymentDto MapToDto(Payment paymemt, Guid tenantId);
    
    public partial Payment MapToEntity(PaymentDto dto, Guid tenantId);
}