using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;


[Authorize]
[ApiController]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IUserService _userService;
    public UserController(IUserService userService)
    {
        _userService = userService;
    }

    [HttpGet(ApiRoutes.User.GetAll, Name = "[controller]_[action]")]
    public async Task<IActionResult> GetAll(/* [FromQuery] GetAllUsersQuery query, [FromQuery] PaginationQuery paginationQuery */)
    {
        return null;
    }

    [HttpGet(ApiRoutes.User.Get, Name = "[controller]_[action]")]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        return null;
    }

    [HttpPut(ApiRoutes.User.Update, Name = "[controller]_[action]")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UserUpdateRequest request)
    {
        return null;
    }

    [HttpDelete(ApiRoutes.User.Delete, Name = "[controller]_[action]")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        return null;
    }
}