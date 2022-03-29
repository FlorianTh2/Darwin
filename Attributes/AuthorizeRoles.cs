using darwin.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace darwin.Attributes;
public class AuthorizeRoles : AuthorizeAttribute
{
    public AuthorizeRoles(params Roles[] allowedRoles)
    {
        var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(Roles), x));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}