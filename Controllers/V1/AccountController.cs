using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    public AccountController()
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.Register)]
    public async Task<ActionResult> Register([FromBody] AccountRegisterRequest request)
    {

    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Account.RegisterConfirm)]
    public async Task<ActionResult> RegisterConfirm([FromQuery] AccountRegisterConfirmRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.Login)]
    public async Task<ActionResult> Login([FromBody] AccountLoginRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.RefreshAccessToken)]
    public async Task<ActionResult> RefreshAccessToken([FromBody] AccountRefreshAccessTokenRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.PasswordReset)]
    public async Task<ActionResult> PasswordReset([FromBody] AccountPasswordResetRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.PasswordResetConfirm)]
    public async Task<ActionResult> PasswordResetConfirm([FromBody] AccountPasswordResetConfirmRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Account.PasswordUpdate)]
    public async Task<ActionResult> PasswordUpdate([FromBody] AccountPasswordUpdateRequest request)
    {

    }

    [HttpPost(ApiRoutes.Account.PasswordUpdateConfirm)]
    public async Task<ActionResult> PasswordUpdateConfirm([FromBody] AccountPasswordUpdateConfirmRequest request)
    {

    }
}