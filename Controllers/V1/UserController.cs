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
    private readonly ICurrentUserService _currentUserService;

    public UserController(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUriService uriService,
        IUserService userService,
        ICurrentUserService currentUserService
    )
    {
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
        _uriService = uriService;
        _userService = userService;
        _currentUserService = currentUserService;
    }

    [HttpGet(ApiRoutes.User.GetAll, Name = "[controller]_[action]")]
    public async Task<ActionResult<PagedResponse<UserResponse>>> GetAll(
        [FromQuery] PaginationQuery paginationQuery,
        [FromQuery] GetAllUsersQuery query
    )
    {
        var paginationFilter = _mapper.Map<PaginationFilter>(paginationQuery);
        var filter = _mapper.Map<GetAllUsersFilter>(query);

        var serviceResponse = await _userService.GetUsersAsync(filter, paginationFilter);
        var usersResponse = _mapper.Map<List<UserResponse>>(serviceResponse.Data);

        return Ok(PaginationHelper.CreatePaginatedResponse(_uriService, ApiRoutes.User.GetAll, paginationFilter, usersResponse, serviceResponse.TotalNumber));

    }

    [HttpGet(ApiRoutes.User.Get, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UserResponse>>> Get([FromRoute] int userId)
    {

        var user = await _userService.GetUserByIdAsync(userId);

        if (user == null)
            return NotFound();

        return Ok(new Response<UserResponse>(_mapper.Map<UserResponse>(user)));
    }

    [HttpPut(ApiRoutes.User.Update, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UserResponse>>> Update(
        [FromRoute] int userId,
        [FromBody] UserUpdateRequest request
    )
    {
        var userOwnsUser = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUser)
        {
            return BadRequest(new ErrorResponse<ErrorModel>()
            {
                Errors = new List<ErrorModel>() {
                    new ErrorModel() { Message = "You can not change user data of another user" }
                }
            });
        }
        var user = (await _userService.GetUserByIdAsync(userId))!;

        // following properties can be updated currently:
        user.DOB = request.DOB.FromIso8601StringToDateTime();

        var updated = await _userService.UpdateUserAsync(user);

        if (updated)
            return Ok(new Response<UserResponse>(_mapper.Map<UserResponse>(user)));

        return NotFound();
    }
}