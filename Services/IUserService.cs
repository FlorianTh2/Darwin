using hello_asp_identity.Domain;

namespace hello_asp_identity.Services;

public interface IUserService
{
        Task<GetProjectsAsyncServiceResponse> GetUsersAsync();

        Task<AppUser> GetUserByIdAsync(Guid userId);

        Task<bool> UpdateUserInDatabase();

        Task<bool> DeleteUserByIdAsync(Guid userId);
}