using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace hello_asp_identity.Domain;

public class RefreshToken
{
    public string Token { get; set; }

    public string JwtId { get; set; }

    public DateTime Created { get; set; }

    public DateTime ExpirationDate { get; set; }

    public bool Used { get; set; }

    public bool Invalidated { get; set; }

    public string UserId { get; set; }

    public AppUser User { get; set; }
}