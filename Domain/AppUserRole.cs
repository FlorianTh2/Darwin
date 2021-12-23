using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Domain;

public class AppUserRole : IdentityUserRole<Guid>
{
    public AppUser User { get; set; }
    public AppRole Role { get; set; }
}