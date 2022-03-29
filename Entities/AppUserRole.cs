using Microsoft.AspNetCore.Identity;

namespace darwin.Entities;

public class AppUserRole : IdentityUserRole<Guid>
{
    public virtual AppUser User { get; set; } = null!;
    public virtual AppRole Role { get; set; } = null!;
}