using AutoMapper;
using hello_asp_identity.Attributes;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain.Enums;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Extensions;
using hello_asp_identity.Helpers;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace hello_asp_identity.Controllers.V1;

public class IdentityController : AppControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IUserService _userService;
    private readonly ICurrentUserService _currentUserService;
    private readonly IIdentityService _identityService;
    private readonly Serilog.ILogger _log = Log.ForContext<IdentityController>();

    public IdentityController(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUriService uriService,
        IUserService userService,
        ICurrentUserService currentUserService,
        IIdentityService identityService
        ) : base(mapper)
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _userService = userService;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register, Name = "[controller]_[action]")]
    public async Task<IActionResult> Register([FromBody] IdentityRegisterRequest request)
    {
        Log.Information("Registration of: {@request}", request);

        var serviceResult = await _identityService.RegisterAsync(
            request.Username,
            request.Email,
            request.Password,
            request.DOB.FromIso8601StringToDateTime()
        );
        return serviceResult.MatchResponse(_mapper);
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.RegisterConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RegisterConfirm([FromQuery] IdentityRegisterConfirmRequest request)
    {
        var serviceResult = await _identityService.RegisterConfirmAsync(request.UserId, request.Token);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<AuthResponse>(
            _mapper.Map<AuthResponse>(serviceResult.Value)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] IdentityLoginRequest request)
    {
        var serviceResult = await _identityService.LoginAsync(request.Username, request.Password);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<AuthResponse>(_mapper.Map<AuthResponse>(serviceResult.Value)));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.RefreshAccessToken, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RefreshAccessToken([FromBody] IdentityRefreshAccessTokenRequest request)
    {
        var serviceResult = await _identityService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<AuthResponse>(_mapper.Map<AuthResponse>(serviceResult.Value)));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordReset, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordResetResponse>>> PasswordReset([FromBody] IdentityPasswordResetRequest request)
    {
        var serviceResult = await _identityService.PasswordResetAsync(request.Email);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<PasswordResetResponse>(
            new PasswordResetResponse { Description = "Started reset password, email sent." }
        ));
    }

    [AuthorizeRoles(Roles.Admin, Roles.SuperAdmin)]
    [HttpPost(ApiRoutes.Identity.PasswordResetByAdmin, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordResetByAdminResponse>>> PasswordResetByAdmin([FromBody] IdentityPasswordResetByAdminRequest request)
    {
        var serviceResult = await _identityService.PasswordResetByAdminAsync(request.Email);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<PasswordResetByAdminResponse>(
            _mapper.Map<PasswordResetByAdminResponse>(serviceResult.Value)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordResetConfirm, Name = "[controller]_[action]")]
    public async Task<IActionResult> PasswordResetConfirm([FromBody] IdentityPasswordResetConfirmRequest request)
    {
        var serviceResult = await _identityService.PasswordResetConfirmAsync(
            request.UserId,
            request.PasswordResetConfirmationToken,
            request.password
        );

        return serviceResult.MatchResponse<PasswordResetConfirmResponse>(
            new PasswordResetConfirmResponse { Description = "Password resetted, please login." }, _mapper
        );
    }

    [HttpPut(ApiRoutes.Identity.PasswordUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordUpdateResponse>>> PasswordUpdate([FromRoute] Guid userId, [FromBody] IdentityPasswordUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (userOwnsUserResult.Failed()) CreateErrorResultByErrorResponse(userOwnsUserResult);

        if (userOwnsUserResult.Value == false)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResult = await _identityService.PasswordUpdateAsync(userId, request.Password, request.NewPassword);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<PasswordUpdateResponse>(
            new PasswordUpdateResponse { Description = "Password updated successfully" }
        ));
    }

    [HttpPut(ApiRoutes.Identity.UsernameUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UsernameUpdateResponse>>> UsernameUpdate([FromRoute] Guid userId, [FromBody] IdentityUsernameUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (userOwnsUserResult.Failed()) CreateErrorResultByErrorResponse(userOwnsUserResult);

        if (userOwnsUserResult.Value == false)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResult = await _identityService.UsernameUpdateAsync(userId, request.NewUsername);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<UsernameUpdateResponse>(
            new UsernameUpdateResponse { Description = "Username updated successfully" }
        ));
    }

    [HttpPut(ApiRoutes.Identity.EmailUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<EmailUpdateResponse>>> EmailUpdate([FromRoute] Guid userId, [FromBody] IdentityEmailUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (userOwnsUserResult.Failed()) CreateErrorResultByErrorResponse(userOwnsUserResult);

        if (userOwnsUserResult.Value == false)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResult = await _identityService.EmailUpdateAsync(userId, request.OldEmail, request.UnConfirmedEmail);

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<EmailUpdateResponse>(
            new EmailUpdateResponse { Description = "Confirmation email sent to new email address." }
        ));
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.EmailUpdateConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<EmailUpdateConfirmResponse>>> EmailUpdateConfirm([FromQuery] IdentityEmailUpdateConfirmRequest request)
    {
        var serviceResult = await _identityService.EmailUpdateConfirmAsync(
            request.UserId,
            request.EmailConfirmationToken
        );

        if (serviceResult.Failed()) CreateErrorResultByErrorResponse(serviceResult);

        return Ok(new Response<EmailUpdateResponse>(
            new EmailUpdateResponse { Description = "Users email updated, pls login again." }
        ));
    }

    [HttpDelete(ApiRoutes.User.Delete, Name = "[controller]_[action]")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (userOwnsUserResult.Failed()) CreateErrorResultByErrorResponse(userOwnsUserResult);

        if (userOwnsUserResult.Value == false)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var deletedResult = await _identityService.DeleteUserByIdAsync(userId);

        if (deletedResult.Failed()) CreateErrorResultByErrorResponse(deletedResult);

        return NoContent(); // 204
    }
}