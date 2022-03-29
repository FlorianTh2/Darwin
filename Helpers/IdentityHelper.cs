using System.Text;
using Darwin.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;

namespace Darwin.Helpers;

public class IdentityHelper
{
    public static string IdentityErrorsToString(IEnumerable<IdentityError> errors)
    {
        return String.Join(", ", errors);
    }
}