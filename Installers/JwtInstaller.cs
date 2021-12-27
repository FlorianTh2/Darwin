using System.Text;
using hello_asp_identity.Helper;
using hello_asp_identity.Options;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace hello_asp_identity.Installers;

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