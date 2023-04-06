using Auth.Core.Extensions;
using Auth.Core.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [AllowAnonymous]
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> SignIn([FromQuery] SignInDto model)
    {
        var provider = await HttpContext.GetAuthenticationProviderName(model.Provider);
        return !string.IsNullOrWhiteSpace(provider)
            // ? Challenge(provider)
            ? Challenge(new AuthenticationProperties
            {
                RedirectUri = "/api/users/me",
            }, provider)
            : BadRequest();
    }
}