using JwtAuth.Models.Dtos;
using JwtAuth.Services;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    private readonly AuthService _authService;

    public AuthController(AuthService authService)
    {
        _authService = authService;
    }

    [HttpPost("register")]
    public async ValueTask<IActionResult> RegisterAsync([FromBody] RegisterDetails registerDetails)
    {
        await _authService.RegisterAsync(registerDetails);
        return Ok();
    }

    [HttpPost("login")]
    public async ValueTask<IActionResult> LoginAsync([FromBody] LoginDetails loginDetails)
    {
        var token = await _authService.LoginAsync(loginDetails);
        return Ok(token);
    }
}