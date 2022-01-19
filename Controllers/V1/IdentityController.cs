using AutoMapper;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Data;
using hello_asp_identity.Entities;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class IdentityController : ControllerBase
{
    private readonly UserManager<AppUser> _userManager;
    private readonly SignInManager<AppUser> _signInManager;
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;

    private readonly Serilog.ILogger _log = Log.ForContext<IdentityController>();

    public IdentityController(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUriService uriService,
        UserManager<AppUser> userManager,
        SignInManager<AppUser> signInManager,
        AppDbContext dbContext
        )
    {
        _userManager = userManager;
        _signInManager = signInManager;
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<RegisterResponse>>> Register([FromBody] IdentityRegisterRequest request)
    {
        Log.Information("Processed {@request}", request);
        return Ok(new Response<RegisterResponse>(new RegisterResponse { Description = "Started registration" }));
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.RegisterConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RegisterConfirm([FromQuery] IdentityRegisterConfirmRequest request)
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] IdentityLoginRequest request)
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.RefreshAccessToken, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RefreshAccessToken([FromBody] IdentityRefreshAccessTokenRequest request)
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordReset, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> PasswordReset([FromBody] IdentityPasswordResetRequest request)
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.PasswordResetConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> PasswordResetConfirm([FromQuery] IdentityPasswordResetConfirmRequest request)
    {
        return Ok();
    }

    [HttpPut(ApiRoutes.Identity.PasswordUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> PasswordUpdate([FromRoute] Guid userId, [FromBody] IdentityPasswordUpdateRequest request)
    {
        // to identitfy user: extract userId from Token

        // var user = await UserManager.FindByIdAsync(id);

        // var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        // var result = await UserManager.ResetPasswordAsync(user, token, "MyN3wP@ssw0rd");
        return Ok();
    }

    [HttpPut(ApiRoutes.Identity.UsernameUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> UsernameUpdate([FromRoute] Guid userId, [FromBody] IdentityUsernameUpdateRequest request)
    {
        return Ok();
    }

    [HttpPut(ApiRoutes.Identity.EmailUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> EmailUpdate([FromRoute] Guid userId, [FromBody] IdentityEmailUpdateRequest request)
    {
        return Ok();
    }

    [HttpGet(ApiRoutes.Identity.EmailUpdateConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response>> EmailUpdateConfirm([FromQuery] IdentityEmailUpdateConfirmRequest request)
    {
        // SendEmailConfirmationWarningAsync
        return Ok();
    }

    // private async Task<string> SendEmailConfirmationWarningAsync(string userID, string subject)
    // {
    //     string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
    //     var callbackUrl = Url.Action("ConfirmEmail", "Account",
    //        new { userId = userID, code = code }, protocol: Request.Url.Scheme);
    //     await UserManager.SendEmailAsync(userID, subject,
    //        "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

    //     return callbackUrl;
    // }
}