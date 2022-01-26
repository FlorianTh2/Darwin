using hello_asp_identity.Domain.Results;
using Microsoft.AspNetCore.Identity;

namespace hello_asp_identity.Extensions;

public static class IdentityResultExtensions
{
    public static Result ToAppResult(this IdentityResult result)
    {
        if (!result.Succeeded)
        {
            return new Result() { Success = false, Errors = result.Errors.Select(a => a.Description).ToList() };
        }
        return new Result() { Success = true };
    }

    public static Result<T> ToAppResult<T>(this IdentityResult result, T data)
    {
        if (!result.Succeeded)
        {
            return new Result<T>() { Success = false, Errors = result.Errors.Select(a => a.Description).ToList() };
        }
        return new Result<T>() { Success = true, Data = data };
    }
}