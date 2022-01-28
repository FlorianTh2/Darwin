using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Entities;

namespace hello_asp_identity.Services;

public interface IUserService
{
    Task<Result<GetAllServiceResult<AppUser>>> GetUsersAsync(
        GetAllUsersFilter filter,
        PaginationFilter paginationFilter
    );

    Task<Result<AppUser>> GetUserByIdAsync(Guid userId);

    // update only non-identity user fields (atm: dob)
    Task<Result<bool>> UpdateUserAsync(AppUser userToUpdate);

    Task<Result<bool>> UserOwnsUserAsync(Guid userId, string userIdRequestAuthor);
}