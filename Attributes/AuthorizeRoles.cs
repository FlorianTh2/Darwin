using hello_asp_identity.Domain.Enums;
using Microsoft.AspNetCore.Authorization;

namespace hello_asp_identity.Attributes;
public class AuthorizeRoles : AuthorizeAttribute
{
    public AuthorizeRoles(params Roles[] allowedRoles)
    {
        var allowedRolesAsStrings = allowedRoles.Select(x => Enum.GetName(typeof(Roles), x));
        Roles = string.Join(",", allowedRolesAsStrings);
    }
}