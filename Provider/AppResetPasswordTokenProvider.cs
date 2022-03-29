using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace darwin.Provider;

public class AppResetPasswordTokenProvider<TUser> : DataProtectorTokenProvider<TUser> where TUser : class
{
    public AppResetPasswordTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<AppResetPasswordTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
        : base(dataProtectionProvider, options, logger)
    {
    }
}

public class AppResetPasswordTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public AppResetPasswordTokenProviderOptions()
    {
        Name = "AppResetPasswordTokenProvider";
        TokenLifespan = TimeSpan.FromMinutes(10);
    }
}