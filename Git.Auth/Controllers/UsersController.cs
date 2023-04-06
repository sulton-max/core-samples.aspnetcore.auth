using Auth.Core.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Octokit;

namespace Git.Auth.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class UsersController : ControllerBase
{
    [HttpGet("[action]")]
    public async ValueTask<IActionResult> Me()
    {
        var client = new GitHubClient(new ProductHeaderValue("test"))
        {
            Credentials = new Credentials(HttpContext.User.GetAccessToken())
        };
        var data = await client.User.Get(User.Identity?.Name);
        return data != null ? Ok(data) : BadRequest();
    }
}