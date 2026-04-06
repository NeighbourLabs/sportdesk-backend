using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sportdesk_backend.Auth;
using sportdesk_backend.Dtos.Auth;
using sportdesk_backend.Enums;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class AuthService(
    IUserRepository userRepository,
    ITenantRepository tenantRepository,
    IEmailService emailService,
    AppDbContext dbContext,
    IOptions<JwtSettings> jwtOptions,
    IOptions<FrontendSettings> frontendOptions) : IAuthService
{
    private readonly JwtSettings _jwt = jwtOptions.Value;
    private readonly FrontendSettings _frontend = frontendOptions.Value;

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, request.TenantId)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task InviteAsync(InviteRequest request)
    {
        var token = GenerateUrlSafeToken();

        var invitation = new RegistrationInvitation
        {
            Id = Guid.NewGuid(),
            Token = token,
            Email = request.Email,
            ExpiresAt = DateTime.UtcNow.AddHours(24),
            IsUsed = false,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.RegistrationInvitations.Add(invitation);
        await dbContext.SaveChangesAsync();

        var registrationLink = $"{_frontend.BaseUrl}/register/{token}";
        await emailService.SendInvitationAsync(request.Email, registrationLink);
    }

    public async Task<bool> ValidateInviteAsync(string token)
    {
        var invitation = await dbContext.RegistrationInvitations
            .FirstOrDefaultAsync(i => i.Token == token);

        return invitation != null && !invitation.IsUsed && invitation.ExpiresAt > DateTime.UtcNow;
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var invitation = await dbContext.RegistrationInvitations
            .FirstOrDefaultAsync(i => i.Token == request.Token)
            ?? throw new InvalidOperationException("Invalid or expired invitation.");

        if (invitation.IsUsed || invitation.ExpiresAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Invalid or expired invitation.");

        await using var transaction = await dbContext.Database.BeginTransactionAsync();

        var tenant = new Tenant
        {
            Id = Guid.NewGuid(),
            Name = request.TenantName
        };
        await tenantRepository.CreateAsync(tenant);

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.Surname,
            Email = invitation.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Tel = request.Tel,
            Role = UserRole.OWNER,
            TenantId = tenant.Id,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };
        await userRepository.CreateAsync(user);

        invitation.IsUsed = true;
        await dbContext.SaveChangesAsync();

        await transaction.CommitAsync();

        return await GenerateAuthResponseAsync(user);
    }

    public async Task ForgotPasswordAsync(ForgotPasswordRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, request.TenantId);
        if (user == null)
            return; // don't reveal whether the email exists

        var token = GenerateUrlSafeToken();

        var resetToken = new PasswordResetToken
        {
            Id = Guid.NewGuid(),
            Token = token,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddHours(1),
            IsUsed = false,
            CreatedAt = DateTime.UtcNow
        };

        dbContext.PasswordResetTokens.Add(resetToken);
        await dbContext.SaveChangesAsync();

        var resetLink = $"{_frontend.BaseUrl}/reset-password/{token}";
        await emailService.SendPasswordResetAsync(request.Email, resetLink);
    }

    public async Task ResetPasswordAsync(ResetPasswordRequest request)
    {
        var resetToken = await dbContext.PasswordResetTokens
            .Include(p => p.User)
            .FirstOrDefaultAsync(p => p.Token == request.Token)
            ?? throw new InvalidOperationException("Invalid or expired reset token.");

        if (resetToken.IsUsed || resetToken.ExpiresAt <= DateTime.UtcNow)
            throw new InvalidOperationException("Invalid or expired reset token.");

        resetToken.User.Password = BCrypt.Net.BCrypt.HashPassword(request.NewPassword);
        resetToken.User.UpdatedAt = DateTime.UtcNow;
        resetToken.IsUsed = true;

        await dbContext.SaveChangesAsync();
    }

    public async Task<AuthResponse> RefreshAsync(RefreshRequest request)
    {
        var storedToken = await dbContext.RefreshTokens
            .Include(r => r.User)
            .FirstOrDefaultAsync(r => r.Token == request.RefreshToken)
            ?? throw new UnauthorizedAccessException("Invalid refresh token.");

        if (storedToken.IsRevoked)
            throw new UnauthorizedAccessException("Refresh token has been revoked.");

        if (storedToken.ExpiresAt < DateTime.UtcNow)
            throw new UnauthorizedAccessException("Refresh token has expired.");

        storedToken.IsRevoked = true;
        await dbContext.SaveChangesAsync();

        return await GenerateAuthResponseAsync(storedToken.User);
    }

    public async Task LogoutAsync(string refreshToken)
    {
        var storedToken = await dbContext.RefreshTokens
            .FirstOrDefaultAsync(r => r.Token == refreshToken);

        if (storedToken != null)
        {
            storedToken.IsRevoked = true;
            await dbContext.SaveChangesAsync();
        }
    }

    private async Task<AuthResponse> GenerateAuthResponseAsync(User user)
    {
        var expiresAt = DateTime.UtcNow.AddMinutes(_jwt.AccessTokenExpirationMinutes);
        var accessToken = GenerateAccessToken(user, expiresAt);
        var refreshToken = GenerateRefreshToken();

        var refreshTokenEntity = new RefreshToken
        {
            Id = Guid.NewGuid(),
            Token = refreshToken,
            UserId = user.Id,
            ExpiresAt = DateTime.UtcNow.AddDays(_jwt.RefreshTokenExpirationDays),
            CreatedAt = DateTime.UtcNow
        };

        dbContext.RefreshTokens.Add(refreshTokenEntity);
        await dbContext.SaveChangesAsync();

        return new AuthResponse
        {
            AccessToken = accessToken,
            RefreshToken = refreshToken,
            ExpiresAt = expiresAt
        };
    }

    private string GenerateAccessToken(User user, DateTime expiresAt)
    {
        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.SecretKey));
        var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

        var claims = new[]
        {
            new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
            new Claim(JwtRegisteredClaimNames.Email, user.Email),
            new Claim("role", user.Role?.ToString() ?? string.Empty),
            new Claim("tenantId", user.TenantId.ToString()),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        };

        var token = new JwtSecurityToken(
            issuer: _jwt.Issuer,
            audience: _jwt.Audience,
            claims: claims,
            expires: expiresAt,
            signingCredentials: credentials);

        return new JwtSecurityTokenHandler().WriteToken(token);
    }

    private static string GenerateRefreshToken()
    {
        var randomBytes = new byte[64];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToBase64String(randomBytes);
    }

    private static string GenerateUrlSafeToken()
    {
        var randomBytes = new byte[32];
        using var rng = RandomNumberGenerator.Create();
        rng.GetBytes(randomBytes);
        return Convert.ToHexString(randomBytes).ToLowerInvariant();
    }
}
