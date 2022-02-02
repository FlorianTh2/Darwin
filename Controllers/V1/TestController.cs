using AutoMapper;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Data;
using hello_asp_identity.Entities;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace hello_asp_identity.Controllers.V1;

[ApiController]
[Produces("application/json")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly Serilog.ILogger _log = Log.ForContext<IdentityController>();

    // https://github.com/aau-giraf/web-api/blob/develop/GirafRest/Controllers/AccountController.cs
    // https://github.com/Leftyx/AspNetIdentityCustomDb
    // https://github.com/iammukeshm/CustomUserManagement.MVC
    public TestController(
        IHttpContextAccessor httpContextAccessor,
        IMapper mapper,
        IUriService uriService,
        AppDbContext dbContext
        )
    {
        _dbContext = dbContext;
        _httpContextAccessor = httpContextAccessor;
        _mapper = mapper;
    }

    [AllowAnonymous]
    [HttpGet(ApiRoutes.TestRessource, Name = "[controller]_[action]")]
    public async Task<ActionResult<Response<TestResponse>>> Test()
    {
        Log.Information("Processed testEndpoint");
        return Ok(await Task.FromResult(new Response<TestResponse>(new TestResponse { Description = "Test Response" })));
    }
}