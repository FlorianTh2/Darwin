using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;

namespace darwin.Provider;

public class AppEmailConfirmationTokenProvider<TUser>
                                       : DataProtectorTokenProvider<TUser> where TUser : class
{
    public AppEmailConfirmationTokenProvider(IDataProtectionProvider dataProtectionProvider,
        IOptions<AppEmailConfirmationTokenProviderOptions> options,
        ILogger<DataProtectorTokenProvider<TUser>> logger)
                                          : base(dataProtectionProvider, options, logger)
    {

    }
}

public class AppEmailConfirmationTokenProviderOptions : DataProtectionTokenProviderOptions
{
    public AppEmailConfirmationTokenProviderOptions()
    {
        Name = "AppEmailConfirmationProvider";
        TokenLifespan = TimeSpan.FromHours(24);
    }
}