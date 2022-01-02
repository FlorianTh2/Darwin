using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain;
using hello_asp_identity.Extensions;
using hello_asp_identity.Services;
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
    public async Task<IActionResult> GetAll(
        [FromQuery] GetAllUsersQuery query,
        [FromQuery] PaginationQuery paginationQuery
    )
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
        var filter = _mapper.Map<GetAllUsersFilter>(query);

        var serviceResponse = await _userService.GetUsersAsync(filter, paginationFilter);
        var usersResponse = _mapper.Map<List<UserResponse>>(serviceResponse.);

        // return perma with PaginationHelpers to have a unified approach
        if (paginationFilter == null || paginationFilter.PageNumber < 1 || paginationFilter.PageSize < 1)
        {
            return Ok(new PagedResponse<UserResponse>(usersResponse));
        }

        return Ok(PaginationHelpers.CreatePaginatedResponse(_uriService, ApiRoutes.User.GetAll, paginationFilter, usersResponse, serviceResponse.TotalProjects));

    }

    [HttpGet(ApiRoutes.User.Get, Name = "[controller]_[action]")]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        return null;
    }

    [HttpPut(ApiRoutes.User.Update, Name = "[controller]_[action]")]
    public async Task<IActionResult> Update([FromRoute] Guid userId, [FromBody] UserUpdateRequest request)
    {
        var userOwnsUser = await _userService.UserOwnsUserAsync(userId, HttpContext.GetUserId());

        if (!userOwnsUser)
        {
            return BadRequest(new ErrorResponse<ErrorModel>(
                new List<ErrorModel>() {
                    new ErrorModel() { Message = "You can not change user data of another user" }
                }
            ));
        }
        var user = await _userService.GetUserByIdAsync(userId);

        // following properties can be updated currently:
        user.Age = request.Age;

        var updated = await _userService.UpdateUserAsync(user);

        if (updated)
            return Ok(new Response<UserResponse>(_mapper.Map<UserResponse>(user)));

        return NotFound();
    }

    [HttpDelete(ApiRoutes.User.Delete, Name = "[controller]_[action]")]
    public async Task<IActionResult> Delete([FromRoute] Guid userId)
    {
        var userOwnsUser = await _userService.UserOwnsUserAsync(userId, HttpContext.GetUserId());

        if (!userOwnsUser)
        {
            return BadRequest(new ErrorResponse<ErrorModel>(
                new List<ErrorModel>() {
                    new ErrorModel() { Message = "You can not change user data of another user" }
                }
            ));
        }

        var deleted = await _userService.DeleteUserByIdAsync(userId);

        if (deleted)
            return NoContent(); // 204

        return NotFound();
    }
}