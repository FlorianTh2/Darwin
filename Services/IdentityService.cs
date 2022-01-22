using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Enums;
using hello_asp_identity.Entities;
using hello_asp_identity.Options;
using hello_asp_identity.Provider;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Serilog;

namespace hello_asp_identity.Services;

public class IdentityService : IIdentityService
{
    private readonly IUserService _userService;
    private readonly UserManager<AppUser> _userManager;
    private readonly TokenValidationParameters _tokenValidationParameters;
    private readonly RoleManager<AppRole> _roleManager;
    private readonly AppDbContext _dbContext;
    private readonly JwtOptions _jwtOptions;
    private readonly IDateTimeService _dateTimeService;
    private readonly IUriService _uriService;
    private readonly AppResetPasswordTokenProviderOptions _resetPasswordTokenProviderOptions;
    private readonly AppEmailConfirmationTokenProviderOptions _emailProviderOptions;

    private readonly Serilog.ILogger _log = Log.ForContext<IdentityService>();

    public IdentityService(
        IUserService userService,
        UserManager<AppUser> userManager,
        TokenValidationParameters tokenValidationParameters,
        RoleManager<AppRole> roleManager,
        AppDbContext dbContext,
        IOptions<JwtOptions> jwtOptions,
        IDateTimeService dateTimeService,
        IOptions<AppEmailConfirmationTokenProviderOptions> emailProviderOptions,
        IOptions<AppResetPasswordTokenProviderOptions> resetPasswordTokenProviderOptions,
        IUriService uriService
    )
    {
        _userService = userService;
        _userManager = userManager;
        _tokenValidationParameters = tokenValidationParameters;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _jwtOptions = jwtOptions.Value;
        _dateTimeService = dateTimeService;
        _uriService = uriService;
        _resetPasswordTokenProviderOptions = resetPasswordTokenProviderOptions.Value;
        _emailProviderOptions = emailProviderOptions.Value;
    }

    public async Task<AuthenticationResult> RegisterAsync(string username, string email, string password, DateTime dob)
    {
        var userInSystem0 = await _userManager.FindByEmailAsync(email.ToLower());

        var userInSystem = await _dbContext
            .Users
            .FirstOrDefaultAsync(a => a.UserName == username || a.Email == email);

        if (userInSystem != null)
        {
            return new AuthenticationResult
            {
                Success = false,
                Errors = new[] { "User with this email address already exists" }
            };
        }

        var user = new AppUser()
        {
            UserName = username,
            Email = email,
            DOB = dob
        };

        var identityResult_userCreation = await _userManager.CreateAsync(user, password);

        if (!identityResult_userCreation.Succeeded)
        {
            return new AuthenticationResult
            {
                Errors = identityResult_userCreation.Errors.Select(a => a.Description)
            };
        }

        _log.Information("User created a new account with password.");
        await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenValidTo = _dateTimeService.Now.Add(_emailProviderOptions.TokenLifespan);
        user.EmailConfirmed = false;
        var identityResult_updateEmail = await _userManager.UpdateAsync(user);

        var confirmationLink = _uriService.GetBaseUri()
                               + ApiRoutes.Identity.RegisterConfirm
                               + "?token="
                               + HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8);

        return new AuthenticationResult
        {
            Success = true,
        };
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

    public async Task<bool> IsInRoleAsync(int userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return user != null && await _userManager.IsInRoleAsync(user, role);
    }

    public async Task<bool> DeleteUserByIdAsync(int userId)
    {
        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return false;

        _dbContext.Users.Remove(user);
        var deleted = await _dbContext.SaveChangesAsync();
        return deleted > 0;
    }
}