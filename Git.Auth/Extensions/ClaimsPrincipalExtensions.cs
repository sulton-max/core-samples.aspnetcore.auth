using System.Security.Claims;

namespace Git.Auth.Extensions;

public static class ClaimsPrincipalExtensions
{
    public static string? GetAccessToken(this ClaimsPrincipal principal)
    {
        return principal.Claims.FirstOrDefault(x => x.Type == "access_token")?.Value;
    }
}