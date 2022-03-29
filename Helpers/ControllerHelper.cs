using AutoMapper;
using darwin.Contracts.V1.Responses;
using darwin.Domain.Errors;
using darwin.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace darwin.Helpers;

public class ControllerHelper
{
    public static IActionResult CreateErrorResultByErrorResponse(Result serviceResult, IMapper mapper)
    {
        if (serviceResult.GetOriginError() is NotFoundError)
        {
            return new NotFoundResult();
        }
        return new BadRequestObjectResult(new ErrorResponse<ErrorModelResponse>(
            mapper.Map<List<ErrorModelResponse>>(serviceResult.Errors)
        ));
    }
}