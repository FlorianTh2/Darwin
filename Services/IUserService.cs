using hello_asp_identity.Domain;
using hello_asp_identity.Entities;

namespace hello_asp_identity.Services;

public interface IUserService
{
    Task<GetAllServiceResponse<AppUser>> GetUsersAsync(
        GetAllUsersFilter filter,
        PaginationFilter paginationFilter
    );

    Task<AppUser?> GetUserByIdAsync(int userId);

    // update only non-identity user fields (atm: dob)
    Task<bool> UpdateUserAsync(AppUser userToUpdate);

    Task<bool> UserOwnsUserAsync(int userId, string userIdRequestAuthor);
}