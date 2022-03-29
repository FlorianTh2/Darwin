using System.Text;
using darwin.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace darwin.Helpers;

public class IdentityHelper
{
    public static string IdentityErrorsToString(IEnumerable<IdentityError> errors)
    {
        return String.Join(", ", errors);
    }
}