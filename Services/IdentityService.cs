using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace hello_asp_identity.Services;

public class IdentityService : IIdentityService
{
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTimeService _dateTimeService;

    public IdentityService(
        UserManager<AppUser> userManager,
        TokenValidationParameters tokenValidationParameters,
        RoleManager<AppRole> roleManager,
        AppDbContext dbContext,
        IOptions<JwtOptions> jwtOptions,
        IDateTimeService dateTimeService
    )
    {
        _userManager = userManager;
        _tokenValidationParameters = tokenValidationParameters;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _jwtOptions = jwtOptions.Value;
        _dateTimeService = dateTimeService;
    }

    public Task<AuthenticationResult> RegisterAsync(string username, string email, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> LoginAsync(string username, string password)
    {
        throw new NotImplementedException();
    }

    public Task<AuthenticationResult> RefreshTokenAsync(string token, string refreshToken)
    {
        throw new NotImplementedException();
    }

    private async Task<AuthenticationResult> CreateToken(AppUser appUser)
    {
        // https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
        //  - no pii (personal identifying information) preferable
        var claims = new List<Claim>
            {
                // include to search directly for a possible user foreign key
                new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
                // include to give token an id, later retrievable via token.id
                // if not specified token.id == null
                // token.id needed for refreshtoken
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                // general claims for frontend
                new Claim(JwtRegisteredClaimNames.Email, appUser.Email),
                new Claim("username", appUser.UserName)
            };

        // user-claims
        // - claims added to individual users like for example that way:
        // - e.g. await _userManager.AddClaimAsync(newUser, new Claim("Authorities.view"))
        var userClaims = await _userManager.GetClaimsAsync(appUser);
        claims.AddRange(userClaims);

        // role-claims
        // - add roleClaims to the token
        var userRoles = await _userManager.GetRolesAsync(appUser);
        foreach (var userRole in userRoles)
        {
            // add the userrole-name as a claim to the jwt
            // (userrole-name is for itself not a claim)
            claims.Add(new Claim(ClaimTypes.Role, userRole));

            // each role can have claims which users in that role will inherit
            // included since also needed
            var role = await _roleManager.FindByNameAsync(userRole);
            if (role == null) continue;
            var roleClaims = await _roleManager.GetClaimsAsync(role);

            foreach (var roleClaim in roleClaims)
            {
                if (claims.Contains(roleClaim))
                    continue;

                claims.Add(roleClaim);
            }
        }

        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(claims),
            Expires = _dateTimeService.Now.Add(_jwtOptions.AccessTokenLifetime),
            SigningCredentials = new SigningCredentials(
                new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_jwtOptions.Secret)),
                SecurityAlgorithms.HmacSha512)
        };

        var tokenHandler = new JwtSecurityTokenHandler();
        var token = tokenHandler.CreateToken(tokenDescriptor);

        var refreshToken = new RefreshToken
        {
            JwtId = token.Id,
            UserId = appUser.Id,
            Created = _dateTimeService.Now,
            ExpirationDate = _dateTimeService.Now.Add(_jwtOptions.RefreshTokenLifetime)
        };

        await _dbContext.RefreshTokens.AddAsync(refreshToken);
        await _dbContext.SaveChangesAsync();

        return new AuthenticationResult()
        {
            Success = true,
            Token = tokenHandler.WriteToken(token),
            RefreshToken = refreshToken.Token
        };
    }
}