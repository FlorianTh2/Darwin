using hello_asp_identity.Contracts.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

[ApiController]
[Produces("application/json")]
public class AccountController : ControllerBase
{
    public AccountController()
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Register([FromBody] AccountRegisterRequest request)
    {

    }

    [AllowAnonymous]
    [HttpGet]
    public async Task<ActionResult> RegisterConfirm([FromQuery] AccountRegisterConfirmRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> Login([FromBody] AccountLoginRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> RefreshAccessToken([FromBody] AccountRefreshAccessTokenRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> ForgotPassword([FromBody] AccountForgotPasswordRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> ForgotPasswordWithToken([FromBody] AccountResetPasswordWithTokenRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> UpdatePassword([FromBody] AccountForgotPasswordRequest request)
    {

    }

    [AllowAnonymous]
    [HttpPost]
    public async Task<ActionResult> UpdatePasswordWithToken([FromBody] AccountResetPasswordWithTokenRequest request)
    {

    }
}