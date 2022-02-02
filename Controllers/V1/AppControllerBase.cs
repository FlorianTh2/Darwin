using AutoMapper;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain;
using hello_asp_identity.Domain.Errors;
using hello_asp_identity.Domain.Results;
using hello_asp_identity.Extensions;
using hello_asp_identity.Helpers;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace hello_asp_identity.Controllers.V1;

[Authorize]
[ApiController]
[Produces("application/json")]
public class AppControllerBase : ControllerBase
{
    private readonly IMapper _mapper;

    public AppControllerBase(IMapper mapper)
    {
        _mapper = mapper;
    }

    [NonAction]
    public IActionResult CreateErrorResultByErrorResponse(Result serviceResult)
    {
        if (serviceResult.GetOriginError() is NotFoundError)
        {
            return NotFound();
        }
        return BadRequest(new ErrorResponse<ErrorModelResponse>(
            _mapper.Map<List<ErrorModelResponse>>(serviceResult.Errors)
        ));
    }
}