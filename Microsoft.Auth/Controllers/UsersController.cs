using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Microsoft.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet("[action]")]
    public ValueTask<IActionResult> Me()
    {
        var user = HttpContext.User.Claims.Select(x => new {ClaimType = x.Type, ClaimValue = x.Value});
        return new ValueTask<IActionResult>(Ok(user));
    }
}