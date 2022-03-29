using AutoMapper;
using darwin.Contracts.V1.Responses;
using darwin.Domain.Errors;
using darwin.Domain.Results;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace darwin.Controllers.V1;

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