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
    public UserController()
    {

    }

    [HttpGet(ApiRoutes.User.GetAll)]
    public async Task<IActionResult> GetAll(/* [FromQuery] GetAllUsersQuery query, [FromQuery] PaginationQuery paginationQuery */)
    {
        return null;
    }

    [HttpGet(ApiRoutes.User.Get)]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        return null;
    }

    [HttpPut(ApiRoutes.User.Update)]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UserUpdateRequest request)
    {
        return null;
    }

    [HttpDelete(ApiRoutes.User.Delete)]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        return null;
    }
}