using darwin.Services;
using Microsoft.AspNetCore.Identity;

namespace darwin.Entities;

public class AppRole : IdentityRole<Guid>, IEntity<Guid>
{
    public AppRole() : base() { }

    public AppRole(string roleName) : base(roleName) { }

    public string Comment { get; set; } = string.Empty;

    public DateTime CreatedOn { get; set; }
    public string? CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdaterId { get; set; }

    public virtual IEnumerable<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}