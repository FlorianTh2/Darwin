using System.Globalization;
using System.Reflection;
using Darwin.Extensions;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Localization.Routing;
using Microsoft.OpenApi.Models;

namespace Darwin.Installers;

public static class LocalizationInstaller
{

    public static string DefaultCulture { get; set; } = "en-US";
    public static List<CultureInfo> SupportedCultures { get; set; } = new()
    {
        new(DefaultCulture),
        new("fr-FR"),
        new("de-DE")
    };

    public static void InstallLocalization(this IServiceCollection services)
    {
        services.AddLocalization(options =>
        {
            options.ResourcesPath = "Resources";
        });

        services.Configure<RequestLocalizationOptions>(options =>
        {
            options.DefaultRequestCulture = new RequestCulture(culture: DefaultCulture, uiCulture: DefaultCulture);
            options.SupportedCultures = SupportedCultures;
            options.SupportedUICultures = SupportedCultures;
            options.FallBackToParentCultures = true;
            options.FallBackToParentUICultures = true;

            options.RequestCultureProviders.Insert(0, new CustomRequestCultureProvider(context =>
            {
                var userLangs = context.Request.Headers["Accept-Language"].ToString();
                var firstLang = userLangs.Split(',').FirstOrDefault();
                var defaultLang = string.IsNullOrEmpty(firstLang) ? DefaultCulture : firstLang;
                return Task.FromResult(new ProviderCultureResult(defaultLang, defaultLang))!;
            }));
        });
    }
}