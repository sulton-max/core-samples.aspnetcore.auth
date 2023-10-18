using System.Security.Claims;
using JwtAuth.Constants;

namespace JwtAuth.Extensions;

public static class HttpContextExtensions
{
    public static bool HasUserIdClaim(this ClaimsPrincipal user) => user.HasClaim(c => c.Type == ClaimConstants.UserId);

    public static Guid GetUserIdClaimValue(this ClaimsPrincipal user)
    {
        var value = user.FindFirst(ClaimConstants.UserId)?.Value ?? throw new ArgumentNullException(nameof(user));
        return Guid.Parse(value);
    }
}