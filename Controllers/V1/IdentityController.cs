using AutoMapper;
using hello_asp_identity.Attributes;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain.Enums;
using hello_asp_identity.Extensions;
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
        )
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _userService = userService;
        _currentUserService = currentUserService;
        _identityService = identityService;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<RegisterResponse>>> Register([FromBody] IdentityRegisterRequest request)
    {
        Log.Information("Registration of: {@request}", request);

        var serviceResponse = await _identityService.RegisterAsync(
            request.Username,
            request.Email,
            request.Password,
            request.DOB.FromIso8601StringToDateTime()
        );

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<RegisterResponse>(
            _mapper.Map<RegisterResponse>(serviceResponse.Data)
        ));
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.RegisterConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RegisterConfirm([FromQuery] IdentityRegisterConfirmRequest request)
    {
        var serviceResponse = await _identityService.RegisterConfirmAsync(request.UserId, request.Token);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<AuthResponse>(
            _mapper.Map<AuthResponse>(serviceResponse.Data)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] IdentityLoginRequest request)
    {
        var serviceResponse = await _identityService.LoginAsync(request.Username, request.Password);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<AuthResponse>(
            _mapper.Map<AuthResponse>(serviceResponse.Data)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.RefreshAccessToken, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RefreshAccessToken([FromBody] IdentityRefreshAccessTokenRequest request)
    {
        var serviceResponse = await _identityService.RefreshTokenAsync(request.AccessToken, request.RefreshToken);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<AuthResponse>(
            _mapper.Map<AuthResponse>(serviceResponse.Data)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordReset, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordResetResponse>>> PasswordReset([FromBody] IdentityPasswordResetRequest request)
    {
        var serviceResponse = await _identityService.PasswordResetAsync(request.Email);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<PasswordResetResponse>(
            new PasswordResetResponse { Description = "Started reset password, email sent." }
        ));
    }

    [AuthorizeRoles(Roles.Admin, Roles.SuperAdmin)]
    [HttpPost(ApiRoutes.Identity.PasswordResetByAdmin, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordResetByAdminResponse>>> PasswordResetByAdmin([FromBody] IdentityPasswordResetByAdminRequest request)
    {
        var serviceResponse = await _identityService.PasswordResetByAdminAsync(request.Email);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<PasswordResetByAdminResponse>(
            _mapper.Map<PasswordResetByAdminResponse>(serviceResponse.Data)
        ));
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordResetConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordResetConfirmResponse>>> PasswordResetConfirm([FromBody] IdentityPasswordResetConfirmRequest request)
    {
        var serviceResponse = await _identityService.PasswordResetConfirmAsync(
            request.UserId,
            request.PasswordResetConfirmationToken,
            request.password
        );

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<PasswordResetConfirmResponse>(
            new PasswordResetConfirmResponse { Description = "Password resetted, please login." }
        ));
    }

    [HttpPut(ApiRoutes.Identity.PasswordUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<PasswordUpdateResponse>>> PasswordUpdate([FromRoute] int userId, [FromBody] IdentityPasswordUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResponse = await _identityService.PasswordUpdateAsync(userId, request.Password, request.NewPassword);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        return Ok(new Response<PasswordUpdateResponse>(
            new PasswordUpdateResponse { Description = "Password updated successfully" }
        ));
    }

    [HttpPut(ApiRoutes.Identity.UsernameUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UsernameUpdateResponse>>> UsernameUpdate([FromRoute] int userId, [FromBody] IdentityUsernameUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResponse = await _identityService.UsernameUpdateAsync(userId, request.NewUsername);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        return Ok(new Response<UsernameUpdateResponse>(
            new UsernameUpdateResponse { Description = "Username updated successfully" }
        ));
    }

    [HttpPut(ApiRoutes.Identity.EmailUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<EmailUpdateResponse>>> EmailUpdate([FromRoute] int userId, [FromBody] IdentityEmailUpdateRequest request)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResponse = await _identityService.EmailUpdateAsync(userId, request.OldEmail, request.UnConfirmedEmail);

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        return Ok(new Response<EmailUpdateResponse>(
            new EmailUpdateResponse { Description = "Confirmation email sent to new email address." }
        ));
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.EmailUpdateConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<EmailUpdateConfirmResponse>>> EmailUpdateConfirm([FromQuery] IdentityEmailUpdateConfirmRequest request)
    {
        var serviceResponse = await _identityService.EmailUpdateConfirmAsync(
            request.UserId,
            request.EmailConfirmationToken
        );

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        return Ok(new Response<EmailUpdateResponse>(
            new EmailUpdateResponse { Description = "Users email updated, pls login again." }
        ));
    }

    [HttpDelete(ApiRoutes.User.Delete, Name = "[controller]_[action]")]
    public async Task<IActionResult> Delete([FromRoute] int userId)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var deleted = await _identityService.DeleteUserByIdAsync(userId);

        if (deleted.Success)
            return NoContent(); // 204

        return NotFound();
    }
}