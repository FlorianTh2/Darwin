using Darwin.Data;
using Darwin.Domain;
using Darwin.Domain.Errors;
using Darwin.Domain.Results;
using Darwin.Entities;
using Microsoft.EntityFrameworkCore;

namespace Darwin.Services;

public class UserService : IUserService
{

    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<Result<GetAllServiceResult<AppUser>>> GetUsersAsync(
        GetAllUsersFilter filter,
        PaginationFilter paginationFilter
    )
    {
        var queryable = GetFullUserQuery().AsQueryable();

        queryable = AddFiltersOnQuery(filter, queryable);

        var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

        var serviceResponse = new GetAllServiceResult<AppUser>()
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

        return Result.Ok<GetAllServiceResult<AppUser>>(serviceResponse);
    }

    public async Task<Result<AppUser>> GetUserByIdAsync(Guid userId)
    {
        var user = await GetPlainUserQuery().FirstOrDefaultAsync(a => a.Id == userId);
        if (user == null)
        {
            return Result.Fail<AppUser>(new NotFoundError(nameof(user), userId));
        }

        var data = await GetFullUserQuery().FirstOrDefaultAsync(a => a.Id == userId);
        return Result.Ok<AppUser>(data!);
    }

    public async Task<Result<AppUser>> GetUserByEmailAsync(string email)
    {
        var user = await GetPlainUserQuery().FirstOrDefaultAsync(a => a.Email == email);
        if (user == null)
        {
            return Result.Fail<AppUser>(new NotFoundError(nameof(user), email));
        }

        var data = await GetFullUserQuery().FirstOrDefaultAsync(a => a.Email == email);
        return Result.Ok<AppUser>(data!);
    }

    public async Task<Result<AppUser>> GetUserByUsernameAsync(string username)
    {
        var user = await GetPlainUserQuery().FirstOrDefaultAsync(a => a.UserName == username);
        if (user == null)
        {
            return Result.Fail<AppUser>(new NotFoundError(nameof(user), username));
        }

        var data = await GetFullUserQuery().FirstOrDefaultAsync(a => a.UserName == username);
        return Result.Ok<AppUser>(data!);
    }

    public async Task<Result> UpdateUserAsync(AppUser userToUpdate)
    {
        _dbContext.Users.Update(userToUpdate);
        var updated = await _dbContext.SaveChangesAsync();
        if (updated == 0)
        {
            return Result.Fail(new NoChangesSavedDatabaseError(nameof(userToUpdate), userToUpdate.UserName));
        }
        return Result.Ok();
    }

    private IQueryable<AppUser> GetPlainUserQuery()
    {
        // .AsNoTracking() not possible since some methods that change user depend on this method
        return _dbContext.Users;
    }

    private IQueryable<AppUser> GetFullUserQuery()
    {
        return GetPlainUserQuery()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role);
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

    public async Task<Result<bool>> UserOwnsUserAsync(Guid userId, string userIdRequestAuthor)
    {
        var userResult = await GetUserByIdAsync(userId);

        if (userResult.Failed())
        {
            return Result.Fail<bool>(userResult).AddConsecutiveError(nameof(UserOwnsUserAsync));
        }
        var user = userResult.Value;

        return Result.Ok<bool>(user.Id.ToString() == userIdRequestAuthor);
    }
}