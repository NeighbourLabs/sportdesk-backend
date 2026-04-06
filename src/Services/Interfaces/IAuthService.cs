using sportdesk_backend.Dtos.Auth;

namespace sportdesk_backend.Services.Interfaces;

public interface IAuthService
{
    Task<AuthResponse> LoginAsync(LoginRequest request);
    Task<AuthResponse> RegisterAsync(RegisterRequest request);
    Task<AuthResponse> RefreshAsync(RefreshRequest request);
    Task LogoutAsync(string refreshToken);
    Task InviteAsync(InviteRequest request);
    Task<bool> ValidateInviteAsync(string token);
    Task ForgotPasswordAsync(ForgotPasswordRequest request);
    Task ResetPasswordAsync(ResetPasswordRequest request);
}
