using System.Text;
using darwin.Options;
using Microsoft.IdentityModel.Tokens;

namespace darwin.Helpers;

public class JwtHelper
{
    public static TokenValidationParameters CreateTokenValidationParameters(JwtOptions jwtOptions)
    {
        var tokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(jwtOptions.Secret)),
            ValidateIssuer = false,
            ValidateAudience = false,
            RequireExpirationTime = true,
            ValidateLifetime = true,
            ClockSkew = TimeSpan.Zero
        };
        return tokenValidationParameters;
    }
}