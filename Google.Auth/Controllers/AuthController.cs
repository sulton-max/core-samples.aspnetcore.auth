using Git.Auth.Extensions;
using Git.Auth.Models.Dtos;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;

namespace Google.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController : ControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> SignIn([FromQuery]SignInDto model)
    {
        var provider = await HttpContext.GetAuthenticationProviderName(model.Provider);
        return !string.IsNullOrWhiteSpace(provider)
            ? Challenge(new AuthenticationProperties
            {
                RedirectUri = "/api/users/me"
            })
            : BadRequest();
    }

    [HttpGet("[action]")]
    public new ValueTask<IActionResult> SignOut()
    {
        return new ValueTask<IActionResult>(base.SignOut());
    }
}