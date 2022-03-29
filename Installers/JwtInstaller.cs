using System.Text;
using darwin.Helpers;
using darwin.Options;
using darwin.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace darwin.Installers;

public static class JwtInstaller
{
    public static void InstallJwt(this IServiceCollection services, IConfiguration config)
    {
        var jwtOptions = config.GetSection(JwtOptions.SectionName).Get<JwtOptions>();
        var tokenValidationParameters = JwtHelper.CreateTokenValidationParameters(jwtOptions);
        services.AddSingleton(tokenValidationParameters);

        services.AddAuthentication(a =>
            {
                a.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(b =>
            {
                b.SaveToken = true;
                b.TokenValidationParameters = tokenValidationParameters;
            });

        services.AddAuthorization();
    }
}