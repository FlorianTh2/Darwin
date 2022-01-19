using hello_asp_identity.Data;
using hello_asp_identity.Domain;
using hello_asp_identity.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualBasic;

namespace hello_asp_identity.Services;

public class UserService : IUserService
{

    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<GetAllServiceResponse<AppUser>> GetUsersAsync(
        GetAllUsersFilter filter,
        PaginationFilter paginationFilter
    )
    {
        var queryable = _dbContext
        .Users
        .Include(a => a.UserRoles)
        .ThenInclude(a => a.Role)
        .AsQueryable();

        queryable = AddFiltersOnQuery(filter, queryable);

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        var serviceResponse = new GetAllServiceResponse<AppUser>()
        {
            TotalNumber = await queryable
                .AsNoTracking()
                .LongCountAsync(),
            Data = await queryable
                .AsNoTracking()
                .OrderByDescending(a => a.CreatedOn)
                .Skip(skip)
                .Take(paginationFilter.PageSize)
                .ToListAsync(),
        };

        return serviceResponse;
    }

    public async Task<AppUser?> GetUserByIdAsync(int userId)
    {
        return await _dbContext.Users
            .AsNoTracking()
            .Include(a => a.UserRoles)
            .ThenInclude(a => a.Role)
            .FirstOrDefaultAsync(a => a.Id == userId);
    }

    public async Task<bool> UpdateUserAsync(AppUser userToUpdate)
    {
        _dbContext.Users.Update(userToUpdate);
        var updated = await _dbContext.SaveChangesAsync();
        return updated > 0;
    }

    public async Task<bool> DeleteUserByIdAsync(int userId)
    {
        var user = await GetUserByIdAsync(userId);

        if (user == null)
            return false;

        _dbContext.Users.Remove(user);
        var deleted = await _dbContext.SaveChangesAsync();
        return deleted > 0;
    }

    private IQueryable<AppUser> AddFiltersOnQuery(
        GetAllUsersFilter filter,
        IQueryable<AppUser> queryable
    )
    {
        // filter currently no implemented
        // if (!string.IsNullOrEmpty(filter?.UserId))
        // {
        //     queryable.Where(a => a.UserId == filter.UserId);
        // }

        return queryable;
    }

    public async Task<bool> UserOwnsUserAsync(int userId, string userIdRequestAuthor)
    {
        var user = await _dbContext
            .Users
            .AsNoTracking()
            .FirstOrDefaultAsync(a => a.Id == userId);

        if (user == null)
        {
            return false;
        }

        if (user.Id.ToString() != userIdRequestAuthor)
        {
            return false;
        }

        return true;
    }
}