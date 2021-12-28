using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class IdentityController : ControllerBase
{
    public IdentityController()
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register)]
    public async Task<ActionResult> Register([FromBody] IdentityRegisterRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.RegisterConfirm)]
    public async Task<ActionResult> RegisterConfirm([FromQuery] IdentityRegisterConfirmRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login)]
    public async Task<ActionResult> Login([FromBody] IdentityLoginRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.RefreshAccessToken)]
    public async Task<ActionResult> RefreshAccessToken([FromBody] IdentityRefreshAccessTokenRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordReset)]
    public async Task<ActionResult> PasswordReset([FromBody] IdentityPasswordResetRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordResetConfirm)]
    public async Task<ActionResult> PasswordResetConfirm([FromBody] IdentityPasswordResetConfirmRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordUpdate)]
    public async Task<ActionResult> PasswordUpdate([FromBody] IdentityPasswordUpdateRequest request)
    {
        return null;
    }

    [HttpPost(ApiRoutes.Identity.PasswordUpdateConfirm)]
    public async Task<ActionResult> PasswordUpdateConfirm([FromBody] IdentityPasswordUpdateConfirmRequest request)
    {
        return null;
    }
}