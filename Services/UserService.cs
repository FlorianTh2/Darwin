using hello_asp_identity.Domain;

namespace hello_asp_identity.Services;

public class UserService : IUserService{

    private readonly AppDbContext _dbContext;

    public UserService(AppDbContext dbContext){
        _dbContext = dbContext;
    }

    public async Task<GetProjectsAsyncServiceResponse> GetUsersAsync(){

    }

    public Task<AppUser> GetUserByIdAsync(Guid userId){



            return await _dataContext.Projects
                .SingleOrDefaultAsync(a => a.Id == projectId);

                    // var user = await _userManager.FindByIdAsync(id.ToString());
            // var userRoles = await _userManager.GetRolesAsync(user);

            var user = await Context.Users
                .AsNoTracking()
                .Include(a => a.UserRoles)
                .ThenInclude(a => a.Role)
                .Include(a => a.Files)
                .SingleOrDefaultAsync(a => a.Id == id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(new ServiceResponseViewModel<UsersResponse>(new UsersResponse()
            {
                Id = user.Id,
                Username = user.UserName,
                Salutation = user.Salutation,
                Firstname = user.Firstname,
                Lastname = user.Lastname,
                Created = user.Created,
                Confirmed = user.Confirmed,
                Unlocked = user.Unlocked,
                DepartmentName = user.DepartmentName,
                AuthorityName = user.AuthorityName,
                Company = user.Company,
                Roles = user.UserRoles.Select(c => c.Role.Name),
                FileIds = user.Files.Select(c => c.Id)
            }));

    }

    public Task<bool> UpdateUserInDatabase(){

    }

    public Task<bool> DeleteUserByIdAsync(Guid userId){

    }
}