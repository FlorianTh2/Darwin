using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class IdentityController : ControllerBase
{
    // https://github.com/aau-giraf/web-api/blob/develop/GirafRest/Controllers/AccountController.cs
    public IdentityController()
    {

    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Register, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<BaseResponse>>> Register([FromBody] IdentityRegisterRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.Identity.RegisterConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RegisterConfirm([FromQuery] IdentityRegisterConfirmRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.Login, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> Login([FromBody] IdentityLoginRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.RefreshAccessToken, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<AuthResponse>>> RefreshAccessToken([FromBody] IdentityRefreshAccessTokenRequest request)
    {
        return null;
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordReset, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<BaseResponse>>> PasswordReset([FromBody] IdentityPasswordResetRequest request)
    {
        return Ok();
    }

    [AllowAnonymous]
    [HttpPost(ApiRoutes.Identity.PasswordResetConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult<BaseResponse>> PasswordResetConfirm([FromBody] IdentityPasswordResetConfirmRequest request)
    {
        return null;
    }

    [HttpPost(ApiRoutes.Identity.PasswordUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<BaseResponse>>> PasswordUpdate([FromBody] IdentityPasswordUpdateRequest request)
    {
        // to identitfy user: extract userId from Token

        // var user = await UserManager.FindByIdAsync(id);

        // var token = await UserManager.GeneratePasswordResetTokenAsync(user);

        // var result = await UserManager.ResetPasswordAsync(user, token, "MyN3wP@ssw0rd");
        return null;
    }

    [HttpPost(ApiRoutes.Identity.UsernameUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<BaseResponse>>> UsernameUpdate([FromBody] IdentityUsernameUpdateRequest request)
    {
        return null;
    }

    [HttpPost(ApiRoutes.Identity.EmailUpdate, Name = "[controller]_[action]")]
    public async Task<ActionResult> EmailUpdate([FromBody] IdentityEmailUpdateRequest request)
    {
        return null;
    }

    [HttpPost(ApiRoutes.Identity.EmailUpdateConfirm, Name = "[controller]_[action]")]
    public async Task<ActionResult> EmailUpdateConfirm([FromBody] IdentityEmailUpdateConfirmRequest request)
    {
        // SendEmailConfirmationWarningAsync
        return null;
    }

    private async Task<string> SendEmailConfirmationWarningAsync(string userID, string subject)
    {
        string code = await UserManager.GenerateEmailConfirmationTokenAsync(userID);
        var callbackUrl = Url.Action("ConfirmEmail", "Account",
           new { userId = userID, code = code }, protocol: Request.Url.Scheme);
        await UserManager.SendEmailAsync(userID, subject,
           "Please confirm your account by clicking <a href=\"" + callbackUrl + "\">here</a>");

        return callbackUrl;
    }
}