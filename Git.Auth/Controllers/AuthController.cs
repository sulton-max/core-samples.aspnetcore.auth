using Git.Auth.Extensions;
using Git.Auth.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Git.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> SignIn([FromQuery] SignInDto model)
    {
        var providerName = await HttpContext.GetAuthenticationProviderName(model.Provider);
        return !string.IsNullOrWhiteSpace(providerName)
            ? Challenge(new AuthenticationProperties
            {
                RedirectUri = "/"
            }, providerName)
            : BadRequest();
    }
}