using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using sportdesk_backend.Auth;
using sportdesk_backend.Dtos.Auth;
using sportdesk_backend.Infra;
using sportdesk_backend.Models;
using sportdesk_backend.Repositories.Interfaces;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Services.Implementations;

public class AuthService(
    IUserRepository userRepository,
    AppDbContext dbContext,
    IOptions<JwtSettings> jwtOptions) : IAuthService
{
    private readonly JwtSettings _jwt = jwtOptions.Value;

    public async Task<AuthResponse> LoginAsync(LoginRequest request)
    {
        var user = await userRepository.GetByEmailAsync(request.Email, request.TenantId)
            ?? throw new UnauthorizedAccessException("Invalid credentials.");

        if (!BCrypt.Net.BCrypt.Verify(request.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid credentials.");

        return await GenerateAuthResponseAsync(user);
    }

    public async Task<AuthResponse> RegisterAsync(RegisterRequest request)
    {
        var existing = await userRepository.GetByEmailAsync(request.Email, request.TenantId);
        if (existing != null)
            throw new InvalidOperationException("A user with this email already exists.");

        var user = new User
        {
            Id = Guid.NewGuid(),
            Name = request.Name,
            Surname = request.Surname,
            Email = request.Email,
            Password = BCrypt.Net.BCrypt.HashPassword(request.Password),
            Tel = request.Tel,
            Role = request.Role,
            TenantId = request.TenantId,
            CreatedAt = DateTime.UtcNow,
            UpdatedAt = DateTime.UtcNow
        };

        await userRepository.CreateAsync(user);

        return await GenerateAuthResponseAsync(user);
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
}
