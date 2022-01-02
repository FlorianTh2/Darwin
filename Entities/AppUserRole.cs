using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Entities;

public class AppUserRole : IdentityUserRole<Guid>
{
    public virtual AppUser User { get; set; }
    public virtual AppRole Role { get; set; }
}