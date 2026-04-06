using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using sportdesk_backend.Dtos.Auth;
using sportdesk_backend.Services.Interfaces;

namespace sportdesk_backend.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IAuthService authService) : ControllerBase
{
    [HttpPost("invite")]
    [AllowAnonymous]
    public async Task<IActionResult> Invite(InviteRequest request)
    {
        await authService.InviteAsync(request);
        return NoContent();
    }

    [HttpGet("validate-invite/{token}")]
    [AllowAnonymous]
    public async Task<IActionResult> ValidateInvite(string token)
    {
        var isValid = await authService.ValidateInviteAsync(token);
        if (!isValid)
            return BadRequest(new { message = "Invalid or expired invitation." });
        return Ok();
    }

    [HttpPost("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Login(LoginRequest request)
    {
        try
        {
            var response = await authService.LoginAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException)
        {
            return Unauthorized(new { message = "Invalid credentials." });
        }
    }

    [HttpPost("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Register(RegisterRequest request)
    {
        try
        {
            var response = await authService.RegisterAsync(request);
            return Created("", response);
        }
        catch (InvalidOperationException ex)
        {
            return Conflict(new { message = ex.Message });
        }
    }

    [HttpPost("refresh")]
    [AllowAnonymous]
    public async Task<ActionResult<AuthResponse>> Refresh(RefreshRequest request)
    {
        try
        {
            var response = await authService.RefreshAsync(request);
            return Ok(response);
        }
        catch (UnauthorizedAccessException ex)
        {
            return Unauthorized(new { message = ex.Message });
        }
    }

    [HttpPost("logout")]
    [Authorize]
    public async Task<IActionResult> Logout(RefreshRequest request)
    {
        await authService.LogoutAsync(request.RefreshToken);
        return NoContent();
    }
}
