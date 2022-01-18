using AutoMapper;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain;
using hello_asp_identity.Extensions;
using hello_asp_identity.Helpers;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class UserController : ControllerBase
{
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly IUriService _uriService;
    private readonly IUserService _userService;

    public UserController(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUriService uriService,
        IUserService userService
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _uriService = uriService;
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
        var usersResponse = _mapper.Map<List<UserResponse>>(serviceResponse.Data);

        return Ok(PaginationHelper.CreatePaginatedResponse(_uriService, ApiRoutes.User.GetAll, paginationFilter, usersResponse, serviceResponse.TotalNumber));

    }

    [HttpGet(ApiRoutes.User.Get, Name = "[controller]_[action]")]
    public async Task<IActionResult> Get([FromRoute] Guid userId)
    {
        return null;
    }

    [HttpPut(ApiRoutes.User.Update, Name = "[controller]_[action]")]
    public async Task<IActionResult> Update([FromRoute] int userId, [FromBody] UserUpdateRequest request)
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
        user.DOB = DateTime.Parse(request.DOB, null, System.Globalization.DateTimeStyles.RoundtripKind);

        var updated = await _userService.UpdateUserAsync(user);

        if (updated)
            return Ok(new Response<UserResponse>(_mapper.Map<UserResponse>(user)));

        return NotFound();
    }

    [HttpDelete(ApiRoutes.User.Delete, Name = "[controller]_[action]")]
    public async Task<IActionResult> Delete([FromRoute] int userId)
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