using AutoMapper;
using Darwin.Contracts.V1.Responses;
using Darwin.Domain.Errors;
using Darwin.Domain.Results;
using Microsoft.AspNetCore.Mvc;

namespace Darwin.Helpers;

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