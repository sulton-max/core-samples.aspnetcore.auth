using JwtAuth.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace JwtAuth.Controllers;

[ApiController]
[Authorize]
[Route("api/[controller]")]
public class TestController : ControllerBase
{
    [HttpGet]
    public ValueTask<IActionResult> TestAsync()
    {
        return HttpContext.User.HasUserIdClaim()
            ? new ValueTask<IActionResult>(Ok($"User id is {HttpContext.User.GetUserIdClaimValue()}"))
            : new ValueTask<IActionResult>(BadRequest());
    }
}