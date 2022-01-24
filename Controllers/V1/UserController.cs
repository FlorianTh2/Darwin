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
public class UserController : AppControllerBase
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

        if (!serviceResponse.Success)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(serviceResponse.Errors)
            ));
        }

        var usersResponse = _mapper.Map<List<UserResponse>>(serviceResponse.Data);

        return Ok(PaginationHelper.CreatePaginatedResponse(
            _uriService,
            ApiRoutes.User.GetAll,
            paginationFilter,
            usersResponse,
            serviceResponse.Data.TotalNumber
        ));

    }

    [HttpGet(ApiRoutes.User.Get, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UserResponse>>> Get([FromRoute] int userId)
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var serviceResponse = await _userService.GetUserByIdAsync(userId);

        if (!serviceResponse.Success)
        {
            return NotFound();
        }

        return Ok(new Response<UserResponse>(
            _mapper.Map<UserResponse>(serviceResponse.Data)
        ));
    }

    [HttpPut(ApiRoutes.User.Update, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<UserResponse>>> Update(
        [FromRoute] int userId,
        [FromBody] UserUpdateRequest request
    )
    {
        var userOwnsUserResult = await _userService.UserOwnsUserAsync(userId, _currentUserService.UserId!);

        if (!userOwnsUserResult.Data)
        {
            return BadRequest(new ErrorResponse<ErrorModelResponse>(
                _mapper.Map<List<ErrorModelResponse>>(new List<string> { "You can not access this ressource." })
            ));
        }

        var userResult = (await _userService.GetUserByIdAsync(userId))!;

        // following properties can be updated currently:
        userResult.Data.DOB = request.DOB.FromIso8601StringToDateTime();

        var updated = await _userService.UpdateUserAsync(userResult.Data);

        if (updated.Data)
        {
            return Ok(new Response<UserResponse>(
                _mapper.Map<UserResponse>(userResult.Data)
            ));
        }

        return NotFound();
    }
}