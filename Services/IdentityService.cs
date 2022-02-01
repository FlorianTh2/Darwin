using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Web;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Enums;
using hello_asp_identity.Domain.Errors;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Entities;
using hello_asp_identity.Extensions;
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
    private readonly SignInManager<AppUser> _signInManager;

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
        SignInManager<AppUser> signInManager,
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
        _signInManager = signInManager;
        _tokenValidationParameters = tokenValidationParameters;
        _roleManager = roleManager;
        _dbContext = dbContext;
        _jwtOptions = jwtOptions.Value;
        _dateTimeService = dateTimeService;
        _uriService = uriService;
        _resetPasswordTokenProviderOptions = resetPasswordTokenProviderOptions.Value;
        _emailProviderOptions = emailProviderOptions.Value;
    }

    public async Task<Result<RegisterResult>> RegisterAsync(string username, string email, string password, DateTime dob)
    {
        var userInSystem = await _dbContext
            .Users
            .FirstOrDefaultAsync(a => a.UserName == username || a.Email == email);

        if (userInSystem != null)
        {
            return Result.Fail<RegisterResult>(new AlreadyExistsError(nameof(AppUser), email));
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
            return new Result<RegisterResult>()
            {
                Errors = identityResult_userCreation.Errors.Select(a => a.Description).ToList()
            };
            // return Result.Fail<RegisterResult>(new UserAlreadyExistsException("User with following email address already exists: " + email));

            return Result.Fail<RegisterResult>(new AlreadyExistsError(nameof(AppUser), email));

        }

        _log.Information("User created a new account with password.");
        await _userManager.AddToRoleAsync(user, Roles.Basic.ToString());

        // var token = await _userManager.GenerateUserTokenAsync(user,
        //     _resetPasswordTokenProviderOptions.Name, "ResetPasswordWithToken");
        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenValidTo = _dateTimeService.Now.Add(_emailProviderOptions.TokenLifespan);
        user.EmailConfirmed = false;
        var identityResult_updateEmail = await _userManager.UpdateAsync(user);

        var callbackUrl = _uriService.GetUriWithParameters(
            ApiRoutes.Identity.RegisterConfirm,
            new Dictionary<string, string>
            {
                {"UserId", user.Id.ToString()},
                {"Token", HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8)}
            });

        return new Result<RegisterResult>()
        {
            Success = true,
            Data = new RegisterResult() { CallbackUrl = callbackUrl.OriginalString }
        };
    }

    public async Task<Result<AuthResult>> RegisterConfirmAsync(Guid userId, string token)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;
        if (user == null)
        {
            return new Result<AuthResult>() { Errors = new() { "User not found." } };
        }

        if (user.EmailConfirmed)
        {
            return new Result<AuthResult>() { Errors = new() { "Users email already confirmed." } };
        }
        if (user.EmailConfirmationToken == null || user.EmailConfirmationToken != token)
        {
            return new Result<AuthResult>() { Errors = new() { "EmailConfirmationToken not valid." } };
        }
        if (user.EmailConfirmationTokenValidTo < _dateTimeService.Now)
        {
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result<AuthResult>() { Errors = new() { "EmailConfirmationToken expired." } };
        }
        var identityResult_confirmEmail = await _userManager.ConfirmEmailAsync(user, token);
        if (!identityResult_confirmEmail.Succeeded)
        {
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result<AuthResult>() { Errors = identityResult_confirmEmail.Errors.Select(a => a.Description).ToList() };
        }

        user.EmailConfirmationToken = null;
        user.EmailConfirmationTokenValidTo = null;
        await _userManager.UpdateAsync(user);

        return await CreateToken(user);
    }

    public async Task<Result<AuthResult>> LoginAsync(string username, string password)
    {

        var user = await _userManager.Users.FirstOrDefaultAsync(u => u.UserName == username);

        if (user == null)
        {
            return new Result<AuthResult>() { Errors = new() { "Check your credentials." } };
        }
        var signInResult = await _signInManager.CheckPasswordSignInAsync(user, password, true);
        if (signInResult.IsLockedOut)
        {
            // https://stackoverflow.com/a/66043460/11244995
            // https://stackoverflow.com/questions/22652118/disable-user-in-aspnet-identity-2-0
            // UserManager.IsLockedOutAsync(user.Id)
            return new Result<AuthResult>() { Errors = new() { "User is locked." } };
        }

        if (!signInResult.Succeeded)
        {
            return new Result<AuthResult>() { Errors = new() { "SignIn: unknown error." } };
        }

        return await CreateToken(user);
    }

    public async Task<Result<AuthResult>> RefreshTokenAsync(string accesstoken, string refreshToken)
    {
        var claimsPrincipal = GetPrincipalFromToken(accesstoken);
        if (claimsPrincipal == null)
        {
            return new Result<AuthResult> { Errors = new() { "JWT not valid." } };
        }

        var expiryDateUnix = long.Parse(claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Exp).Value);

        var expiryDateTimeUtc = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc).AddSeconds(expiryDateUnix);

        if (expiryDateTimeUtc > _dateTimeService.Now)
        {
            return new Result<AuthResult> { Errors = new() { "Der JWT ist noch nicht abgelaufen." } };
        }

        var jti = claimsPrincipal.Claims.Single(x => x.Type == JwtRegisteredClaimNames.Jti).Value;

        // returns the stored refreshtoken for this jwt from the database
        var storedRefreshToken = await _dbContext.RefreshTokens.FirstOrDefaultAsync(x => x.Token == refreshToken);

        if (storedRefreshToken == null)
        {
            return new Result<AuthResult> { Errors = new() { "Refreshtoken not existing." } };
        }

        if (_dateTimeService.Now > storedRefreshToken.ExpirationDate)
        {
            return new Result<AuthResult> { Errors = new() { "Refreshtoken expired." } };
        }

        if (storedRefreshToken.Invalidated)
        {
            return new Result<AuthResult> { Errors = new() { "Refreshtoken invalidated." } };
        }

        if (storedRefreshToken.Used)
        {
            return new Result<AuthResult> { Errors = new() { "Refreshtoken already used." } };
        }

        if (storedRefreshToken.JwtId != jti)
        {
            return new Result<AuthResult> { Errors = new() { "Refreshtoken is referencing another JWT" } };
        }

        storedRefreshToken.Used = true;
        _dbContext.RefreshTokens.Update(storedRefreshToken);
        await _dbContext.SaveChangesAsync();

        var userServiceResponse = await _userService.GetUserByIdAsync(
            Guid.Parse(claimsPrincipal.Claims.Single(x => x.Type == ClaimTypes.NameIdentifier).Value)
        );
        var user = userServiceResponse.Data;
        if (user == null)
        {
            return new Result<AuthResult> { Errors = new() { "User of expired JWT does not exist anymore." } };
        }

        return await CreateToken(user);
    }

    public async Task<Result<PasswordResetResult>> PasswordResetAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new Result<PasswordResetResult> { Errors = new() { "User not found." } };
        }

        var resetToken = await _userManager.GeneratePasswordResetTokenAsync(user);

        user.ResetPasswordToken = resetToken;
        user.ResetPasswordTokenValidTo = _dateTimeService.Now.Add(_resetPasswordTokenProviderOptions.TokenLifespan);
        var update_result = await _userManager.UpdateAsync(user);

        if (!update_result.Succeeded)
        {
            return new Result<PasswordResetResult> { Errors = update_result.Errors.Select(a => a.Description).ToList() };
        }
        var callbackUrl = _uriService.GetUriWithParameters(
            ApiRoutes.Identity.RegisterConfirm,
            new Dictionary<string, string>
            {
                {"UserId", user.Id.ToString()},
                {"Token", HttpUtility.UrlEncode(resetToken, System.Text.Encoding.UTF8)}
            });

        return new Result<PasswordResetResult>()
        {
            Success = true,
            Data = new PasswordResetResult() { CallbackUrl = callbackUrl.OriginalString }
        };
    }

    public async Task<Result> PasswordResetConfirmAsync(Guid userId, string token, string password)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;

        if (user == null)
        {
            return new Result { Errors = new() { "User not found." } };
        }
        if (user.ResetPasswordToken == null)
        {
            return new Result { Errors = new() { "Reset password token not found." } };
        }
        if (user.ResetPasswordToken == token)
        {
            return new Result { Errors = new() { "Given reset password token does not match the stored reset password token." } };
        }
        if (user.ResetPasswordTokenValidTo < _dateTimeService.Now)
        {
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result { Success = false, Errors = new() { "Reset password token expired." } };
        }

        var identityResult_resetPassword = await _userManager.ResetPasswordAsync(user, token, password);

        if (!identityResult_resetPassword.Succeeded)
        {
            user.ResetPasswordToken = null;
            user.ResetPasswordTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result { Success = false, Errors = new() { "Unknown server error." } };
        }

        await InvalidateRefreshtokensAsync(user.Id);

        // return identityResult_resetPassword.ToAppResult();
        return new Result() { Success = true };

    }

    public async Task<Result<PasswordResetByAdminResult>> PasswordResetByAdminAsync(string email)
    {
        AppUser user = await _userManager.FindByEmailAsync(email);
        if (user == null)
        {
            return new Result<PasswordResetByAdminResult> { Errors = new() { "User not found." } };
        }

        var token = await _userManager.GeneratePasswordResetTokenAsync(user);

        var newPassword = Password.Generate(16, 4);

        var identityResult_resetPasswordByAdmin = await _userManager.ResetPasswordAsync(user, token, newPassword);

        if (!identityResult_resetPasswordByAdmin.Succeeded)
        {
            return new Result<PasswordResetByAdminResult> { Errors = new() { "Unknown error." } };
        }

        user.ResetPasswordToken = null;
        user.ResetPasswordTokenValidTo = null;
        var updateResult = await _userManager.UpdateAsync(user);

        await InvalidateRefreshtokensAsync(user.Id);

        return new Result<PasswordResetByAdminResult> { Success = true, Data = new PasswordResetByAdminResult { NewPassword = newPassword } };

    }

    public async Task<Result> PasswordUpdateAsync(Guid userId, string password, string newPassword)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;
        if (user == null)
        {
            return new Result { Errors = new() { "User not found." } };
        }

        var identityResult_changePassword = await _userManager.ChangePasswordAsync(user, password, newPassword);
        if (!identityResult_changePassword.Succeeded)
        {
            return new Result { Errors = identityResult_changePassword.Errors.Select(a => a.Description).ToList() };
        }

        await InvalidateRefreshtokensAsync(user.Id);

        return new Result() { Success = true };
    }

    public async Task<Result> UsernameUpdateAsync(Guid userId, string newUsername)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;
        if (user == null)
        {
            return new Result { Errors = new() { "User not found." } };
        }
        user.UserName = newUsername;
        var identityResult_updateUser = await _userManager.UpdateAsync(user);
        if (!identityResult_updateUser.Succeeded)
        {
            return new Result { Errors = identityResult_updateUser.Errors.Select(a => a.Description).ToList() };
        }

        await InvalidateRefreshtokensAsync(user.Id);

        return new Result() { Success = true };
    }

    public async Task<Result<EmailResetResult>> EmailUpdateAsync(Guid userId, string oldEmail, string unConfirmedEmail)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;
        if (user == null)
        {
            return new Result<EmailResetResult> { Errors = new() { "User not found." } };
        }

        if (user.Email != oldEmail)
        {
            return new Result<EmailResetResult> { Errors = new() { "Given oldEmail does not correspond to the email the user with given userId is using." } };
        }

        var userInSystem = await _userManager.FindByEmailAsync(unConfirmedEmail);
        if (userInSystem != null)
        {
            return new Result<EmailResetResult> { Errors = new() { "User with this email address already exists" } };
        }

        var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

        user.UnConfirmedEmail = unConfirmedEmail;
        user.EmailConfirmationToken = token;
        user.EmailConfirmationTokenValidTo = _dateTimeService.Now.Add(_emailProviderOptions.TokenLifespan);
        var identityResult_updateUser = await _userManager.UpdateAsync(user);
        if (!identityResult_updateUser.Succeeded)
        {
            return new Result<EmailResetResult> { Errors = identityResult_updateUser.Errors.Select(a => a.Description).ToList() };
        }

        var callbackUrl = _uriService.GetUriWithParameters(
            ApiRoutes.Identity.RegisterConfirm,
            new Dictionary<string, string>
            {
                {"UserId", user.Id.ToString()},
                {"EmailConfirmationToken", HttpUtility.UrlEncode(token, System.Text.Encoding.UTF8)}
            });

        return new Result<EmailResetResult>
        {
            Success = true,
            Data = new EmailResetResult() { CallbackUrl = callbackUrl.OriginalString }
        };
    }

    public async Task<Result> EmailUpdateConfirmAsync(Guid userId, string token)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;
        if (user == null)
        {
            return new Result() { Success = false, Errors = new() { "User not found." } };
        }

        if (string.IsNullOrEmpty(user.UnConfirmedEmail))
        {
            return new Result() { Errors = new() { "User has no newEmail stored." } };
        }
        if (user.EmailConfirmationToken == null || user.EmailConfirmationToken != token)
        {
            return new Result() { Errors = new() { "EmailConfirmationToken not valid." } };
        }
        if (user.EmailConfirmationTokenValidTo < _dateTimeService.Now)
        {
            user.UnConfirmedEmail = null;
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result { Errors = new() { "EmailConfirmationToken expired." } };
        }
        var identityResult_confirmNewEmail = await _userManager.ConfirmEmailAsync(user, token);
        if (!identityResult_confirmNewEmail.Succeeded)
        {
            user.UnConfirmedEmail = null;
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result() { Errors = identityResult_confirmNewEmail.Errors.Select(a => a.Description).ToList() };
        }
        user.Email = user.UnConfirmedEmail;
        user.UnConfirmedEmail = null;
        user.EmailConfirmationToken = null;
        user.EmailConfirmationTokenValidTo = null;
        var identityResult_updateUser = await _userManager.UpdateAsync(user);

        if (!identityResult_updateUser.Succeeded)
        {
            user.UnConfirmedEmail = null;
            user.EmailConfirmationToken = null;
            user.EmailConfirmationTokenValidTo = null;
            await _userManager.UpdateAsync(user);
            return new Result() { Errors = identityResult_updateUser.Errors.Select(a => a.Description).ToList() };
        }

        await InvalidateRefreshtokensAsync(user.Id);

        return new Result() { Success = true };

    }

    private ClaimsPrincipal? GetPrincipalFromToken(string token)
    {
        var tokenHandler = new JwtSecurityTokenHandler();

        try
        {
            var tokenValidationParameters = _tokenValidationParameters.Clone();
            // set validate lifetime false since otherwise expired token will never be validated positivly
            tokenValidationParameters.ValidateLifetime = false;
            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out var validatedToken);

            // check for proper secruity-algorithm since in a jwt, you can specify secruityAlgorithm: none
            if (!IsJwtWithValidSecrurityAlgorithm(validatedToken))
            {
                return null;
            }
            return principal;
        }
        catch (Exception)
        {
            return null;
        }
    }

    private bool IsJwtWithValidSecrurityAlgorithm(SecurityToken validatedToken)
    {
        return (validatedToken is JwtSecurityToken jwtSecurityToken) &&
               jwtSecurityToken.Header.Alg.Equals(SecurityAlgorithms.HmacSha512,
                   StringComparison.InvariantCultureIgnoreCase);
    }

    private async Task<Result<AuthResult>> CreateToken(AppUser appUser)
    {
        // https://openid.net/specs/openid-connect-core-1_0.html#StandardClaims
        //  - no pii (personal identifying information) preferable
        var claims = new List<Claim>
        {
            // include to give token an id, later retrievable via token.id
            // token.id needed for refreshtoken
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
            new Claim(JwtRegisteredClaimNames.Sub, appUser.Id.ToString()),
            new Claim("username", appUser.UserName),
            new Claim(JwtRegisteredClaimNames.Email, appUser.Email)
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

        return new Result<AuthResult>()
        {
            Success = true,
            Data = new AuthResult()
            {
                AccessToken = tokenHandler.WriteToken(token),
                RefreshToken = refreshToken.Token
            }
        };
    }

    public async Task<Result<bool>> IsInRoleAsync(Guid userId, string role)
    {
        var user = _userManager.Users.SingleOrDefault(u => u.Id == userId);

        return new Result<bool>()
        {
            Success = true,
            Data = user != null && await _userManager.IsInRoleAsync(user, role)
        };
    }

    public async Task<Result<bool>> DeleteUserByIdAsync(Guid userId)
    {
        var user = (await _userService.GetUserByIdAsync(userId)).Data;

        if (user == null)
        {
            return new Result<bool>() { Errors = new() { "User not found." } };
        }

        _dbContext.Users.Remove(user);
        var deleted = await _dbContext.SaveChangesAsync();
        return new Result<bool>() { Success = true, Data = deleted > 0 };
    }

    public async Task<Result> InvalidateRefreshtokensAsync(Guid userId)
    {
        // currently only invalidating refreshToken so that
        // currently valid jwt cannot be extended with its attached refreshToken
        var refreshTokens = _dbContext.RefreshTokens.AsQueryable()
            .Where(a => a.UserId == userId)
            .ToList();

        refreshTokens.ForEach(a => a.Invalidated = true);
        await _dbContext.SaveChangesAsync();

        return new Result() { Success = true };
    }
}