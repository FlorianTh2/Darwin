using System.IdentityModel.Tokens.Jwt;

namespace hello_asp_identity.Extensions;

public static class HttpContextExtensions
{
    public static string GetUserId(this HttpContext a)
    {
        if (a.User == null)
        {
            return string.Empty;
        }

        // previos == "id"
        return a.User.Claims.Single(a => a.Type == JwtRegisteredClaimNames.Sub).Value;

    }
}