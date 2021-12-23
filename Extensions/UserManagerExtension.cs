using hello_asp_identity.Domain;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Extensions;

public static class UserManagerExtension
{
    public static string FindIdByEmailConfirmationToken(
        this UserManager<AppUser> manager,
        string token_code
    )
    {
        var user = manager.Users.SingleOrDefault(a => a.ConfirmedCode != null && a.ConfirmedCode == token_code);
        return user != null ? user.Id.ToString() : null;
    }
}