using hello_asp_identity.Services;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Entities;

public class AppRole : IdentityRole<int>, IEntity<int>
{
    public AppRole() : base()
    {

    }

    public string Comment { get; set; }

    public string CreatorId { get; set; }
    public DateTime CreatedOn { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string UpdaterId { get; set; }

    public virtual ICollection<AppUserRole> UserRoles { get; set; }
}