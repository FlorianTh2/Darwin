using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Domain;

public class AppRole : IdentityRole<Guid>
{
    public ICollection<AppUserRole> UserRoles { get; set; }
    public string Comment { get; set; }
    public DateTime CreatedOn { get; set; }
    public string CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string UpdaterId { get; set; }
    public DateTime? DeletedOn { get; set; }
    public string DeleterId { get; set; }
}