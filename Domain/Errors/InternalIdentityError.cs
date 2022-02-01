using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Domain.Errors;

// class to handle asp.net identity errors
public class InternalIdentityError : DomainError
{
    public List<string> IdentityErrors { get; } = new List<string>();

    public InternalIdentityError(IEnumerable<IdentityError> identityErrors)
        : base("While processing the entity following errors got emitted: [" + String.Join(", ", identityErrors + "]."))
    {
        IdentityErrors = identityErrors.Select(a => a.Description).ToList();
    }
}