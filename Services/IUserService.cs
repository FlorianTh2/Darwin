using hello_asp_identity.Domain;
using hello_asp_identity.Entities;

namespace hello_asp_identity.Services;

public interface IUserService
{
    Task<GetAllAsyncServiceResponse<AppUser>> GetUsersAsync(
        GetAllUsersFilter filter = null,
        PaginationFilter paginationFilter = null
    );

    Task<AppUser> GetUserByIdAsync(Guid userId);

    Task<bool> UpdateUserAsync(AppUser userToUpdate);

    Task<bool> DeleteUserByIdAsync(Guid userId);

    Task<bool> UserOwnsUserAsync(Guid userId, string userIdRequestAuthor);
}