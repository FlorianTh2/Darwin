using System.IdentityModel.Tokens.Jwt;

namespace darwin.Services;

public class CurrentUserService : ICurrentUserService
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUserService(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId
    {
        get
        {
            return _httpContextAccessor.HttpContext?.User?.Claims?.First(a => a.Type == JwtRegisteredClaimNames.Sub)?.Value;
        }
    }
}