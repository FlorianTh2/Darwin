using AutoMapper;
using Darwin.Contracts.V1;
using Darwin.Contracts.V1.Requests;
using Darwin.Contracts.V1.Responses;
using Darwin.Data;
using Darwin.Entities;
using Darwin.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Serilog;

namespace Darwin.Controllers.V1;

[ApiController]
[Produces("application/json")]
public class TestController : ControllerBase
{
    private readonly AppDbContext _dbContext;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IMapper _mapper;
    private readonly Serilog.ILogger _log = Log.ForContext<TestController>();

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