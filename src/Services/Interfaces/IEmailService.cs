namespace sportdesk_backend.Services.Interfaces;

public interface IEmailService
{
    Task SendInvitationAsync(string toEmail, string registrationLink);
    Task SendPasswordResetAsync(string toEmail, string resetLink);
}
