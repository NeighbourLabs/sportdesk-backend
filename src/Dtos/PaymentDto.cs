namespace sportdesk_backend.Dtos;

public sealed record PaymentDto : DtoBase
{ 
    public Guid AthleteEnrollmentId { get; init; } = Guid.Empty;
    public int Months { get; init; }
}