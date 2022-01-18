using hello_asp_identity.Domain;
using hello_asp_identity.Entities;

namespace hello_asp_identity.Services;

public interface IUserService
{
    Task<GetAllAsyncServiceResponse<AppUser>> GetUsersAsync(
        GetAllUsersFilter filter = null,
        PaginationFilter paginationFilter = null
    );

    Task<AppUser> GetUserByIdAsync(int userId);

    Task<bool> UpdateUserAsync(AppUser userToUpdate);

    Task<bool> DeleteUserByIdAsync(int userId);

    Task<bool> UserOwnsUserAsync(int userId, string userIdRequestAuthor);
}