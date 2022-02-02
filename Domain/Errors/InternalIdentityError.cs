using hello_asp_identity.Helpers;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Domain.Errors;

// class to handle asp.net identity errors
public class InternalIdentityError : DomainError
{
    public List<string> IdentityErrors { get; } = new List<string>();

    public InternalIdentityError(IEnumerable<IdentityError> identityErrors)
        : base("While processing the entity following errors got emitted: [" + IdentityHelper.IdentityErrorsToString(identityErrors) + "].")
    {
        IdentityErrors = identityErrors.Select(a => a.Description).ToList();
    }

    // Some identity-results do not offer errors when failing (e.g. _signInManager.CheckPasswordSignInAsync)
    // in that case, just emit a custom message
    public InternalIdentityError(string message) : base(message) { }
}