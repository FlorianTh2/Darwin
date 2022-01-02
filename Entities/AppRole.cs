using hello_asp_identity.Services;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Entities;

public class AppRole : IdentityRole<Guid>
{
    public AppRole() : base()
    {

    }

    public virtual ICollection<AppUserRole> UserRoles { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string UpdaterId { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string DeleterId { get; set; }
}