// using hello_asp_identity.Data;
// using hello_asp_identity.Domain;
// using hello_asp_identity.Entities;
// using Microsoft.EntityFrameworkCore;

// namespace hello_asp_identity.Services;

// public class Vacancy : BaseEntity<Guid>
// {
//     public string Title { get; set; }
// }

// public class GetAllVacanciesFilter { }

// public interface IVacancyService
// {
//     Task<GetAllServiceResponse<Vacancy>> GetVacanciesAsync(
//         GetAllVacanciesFilter filter,
//         PaginationFilter paginationFilter
//     );

//     Task<AppUser?> GetVacancyByIdAsync(Guid vacancyId);

//     Task<bool> UpdateVacancyAsync(Vacancy vacancyToUpdate);

//     Task<bool> DeleteVacancyByIdAsync(Guid vacancyId);
// }

// public class VacancyService : IVacancyService
// {

//     private readonly AppDbContext _dbContext;

//     public VacancyService(AppDbContext dbContext)
//     {
//         _dbContext = dbContext;
//     }

//     public async Task<GetAllServiceResponse<AppUser>> GetVacancyAsync(
//         GetAllVacanciesFilter filter,
//         PaginationFilter paginationFilter
//     )
//     {
//         var queryable = _dbContext
//         .Vacancy
//         .AsQueryable();

//         queryable = AddFiltersOnQuery(filter, queryable);

//         var skip = (paginationFilter.PageNumber - 1) * paginationFilter.PageSize;

//         var serviceResponse = new GetAllServiceResponse<Vacancy>()
//         {
//             TotalNumber = await queryable
//                 .AsNoTracking()
//                 .LongCountAsync(),
//             Data = await queryable
//                 .AsNoTracking()
//                 .OrderByDescending(a => a.CreatedOn)
//                 .Skip(skip)
//                 .Take(paginationFilter.PageSize)
//                 .ToListAsync(),
//         };

//         return serviceResponse;
//     }

//     public async Task<AppUser?> GetUserByIdAsync(Guid userId)
//     {
//         return await _dbContext.Vacancy
//             .AsNoTracking()
//             .Include(a => a.UserRoles)
//             .ThenInclude(a => a.Role)
//             .FirstOrDefaultAsync(a => a.Id == userId);
//     }

//     public async Task<bool> VacancyUserAsync(Vacancy vacancyToUpdate)
//     {
//         _dbContext.Vacancy.Update(vacancyToUpdate);
//         var updated = await _dbContext.SaveChangesAsync();
//         return updated > 0;
//     }

//     public async Task<bool> DeleteVacancyByIdAsync(Guid vacancyId)
//     {
//         var vacancy = await GetVacancyByIdAsync(vacancyId);

//         if (vacancy == null)
//             return false;

//         _dbContext.Vacancy.Remove(vacancy);
//         var deleted = await _dbContext.SaveChangesAsync();
//         return deleted > 0;
//     }

//     private IQueryable<Vacancy> AddFiltersOnQuery(
//         GetAllVacanciesFilter filter,
//         IQueryable<Vacancy> queryable
//     )
//     {
//         // filter currently no implemented
//         // if (!string.IsNullOrEmpty(filter?.UserId))
//         // {
//         //     queryable.Where(a => a.UserId == filter.UserId);
//         // }

//         return queryable;
//     }
// }