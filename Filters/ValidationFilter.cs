using Microsoft.AspNetCore.Mvc.Filters;

namespace hello_asp_identity.Filters;

public class ValidationFilter : IAsyncActionFilter
{
    public Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
    {
        // // here the validator has already been runned
        // if (!context.ModelState.IsValid)
        // {
        //     var errorsInModelState = context.ModelState
        //         .Where(a => a.Value.Errors.Count > 0)
        //         .ToDictionary(keyValuePair => keyValuePair.Key,
        //             keyValuePair => keyValuePair.Value.Errors.Select(b => b.ErrorMessage))
        //         .ToArray();

        //     var errorResponse = new ErrorResponse();

        //     foreach (var error in errorsInModelState)
        //     {
        //         foreach (var subError in error.Value)
        //         {
        //             var errorModel = new ErrorModel()
        //             {
        //                 FieldName = error.Key,
        //                 Message = subError
        //             };

        //             errorResponse.Errors.Add(errorModel);
        //         }
        //     }

        //     context.Result = new BadRequestObjectResult(errorResponse);
        //     return;
        // }

        // // here: before controller
        // // next middleware "downwards" to the application
        // await next();
        // // next middleware "upwards" to the user

        throw new NotImplementedException();
    }
}