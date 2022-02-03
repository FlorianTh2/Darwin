using System.Text;
using AutoMapper;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain.Errors;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Options;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace hello_asp_identity.Helpers;

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