using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Domain;

public class AppUserRole : IdentityUserRole<Guid>
{
    public virtual AppUser User { get; set; }
    public virtual AppRole Role { get; set; }
}