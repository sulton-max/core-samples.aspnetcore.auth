using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Google.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet("[action]")]
    public ValueTask<IActionResult> Me()
    {
        var identity = HttpContext.User;
        return new ValueTask<IActionResult>(Ok(identity.Claims.Select(x => x.ToString())));
    }
}