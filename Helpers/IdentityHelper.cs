using System.Text;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace hello_asp_identity.Helpers;

public class IdentityHelper
{
    public static string IdentityErrorsToString(IEnumerable<IdentityError> errors)
    {
        return String.Join(", ", errors);
    }
}