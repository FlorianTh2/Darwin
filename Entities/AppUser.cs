using Microsoft.AspNetCore.Identity;

namespace darwin.Entities;

public class AppUser : IdentityUser<Guid>, IEntity<Guid>
{
    public DateTime DOB { get; set; }
    public string? EmailConfirmationToken { get; set; }
    public DateTime? EmailConfirmationTokenValidTo { get; set; }
    public string? ResetPasswordToken { get; set; }
    public DateTime? ResetPasswordTokenValidTo { get; set; }
    public bool Suspended { get; set; }

    // used to store new email when user wants to transition to new email
    public string? UnConfirmedEmail { get; set; }

    public DateTime CreatedOn { get; set; }
    public string? CreatorId { get; set; }
    public DateTime? UpdatedOn { get; set; }
    public string? UpdaterId { get; set; }

    public virtual IEnumerable<AppUserRole> UserRoles { get; set; } = new List<AppUserRole>();
}