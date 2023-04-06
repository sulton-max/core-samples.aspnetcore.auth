using Microsoft.AspNetCore.Authentication;

namespace Git.Auth.Extensions;

public static class HttpContextExtensions
{
    #region Authentication

    public async static ValueTask<IEnumerable<AuthenticationScheme>> GetAuthenticationProviders(this HttpContext context)
    {
        var provider = context.RequestServices.GetService<IAuthenticationSchemeProvider>();
        return provider is not null
            ? (await provider.GetAllSchemesAsync()).Where(x => !string.IsNullOrWhiteSpace(x.DisplayName))
            : Enumerable.Empty<AuthenticationScheme>();
    }

    public static async ValueTask<bool> IsSupportedAuthenticationProvider(this HttpContext context, string provider)
    {
        return (await context.GetAuthenticationProviders()).Any(x => x?.DisplayName?.Equals(provider, StringComparison.OrdinalIgnoreCase) ?? false);
    }

    public static async ValueTask<string?> GetProviderName(this HttpContext context, string provider)
    {
        return (await context.GetAuthenticationProviders()).FirstOrDefault(x =>
                x?.DisplayName?.Equals(provider, StringComparison.OrdinalIgnoreCase) ?? false)
            ?.Name ?? default;
    }

    #endregion
}