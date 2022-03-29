using darwin.Domain;
using darwin.Domain.Results;
using darwin.Entities;

namespace darwin.Services;

public interface IUserService
{
    Task<Result<GetAllServiceResult<AppUser>>> GetUsersAsync(
        GetAllUsersFilter filter,
        PaginationFilter paginationFilter
    );

    Task<Result<AppUser>> GetUserByIdAsync(Guid userId);

    Task<Result<AppUser>> GetUserByEmailAsync(string email);

    Task<Result<AppUser>> GetUserByUsernameAsync(string username);

    // update only non-identity user fields (atm: dob)
    Task<Result> UpdateUserAsync(AppUser userToUpdate);

    Task<Result<bool>> UserOwnsUserAsync(Guid userId, string userIdRequestAuthor);
}