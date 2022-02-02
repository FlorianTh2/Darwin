using AutoMapper;
using hello_asp_identity.Contracts.V1;
using Microsoft.AspNetCore.Mvc;

namespace hello_asp_identity.Controllers.V1;

public class HomeController : AppControllerBase
{
    public HomeController(IMapper mapper) : base(mapper) { }

    // start eventuell an anderer route
    // redirect eventuell zu anderer route

    [HttpGet(ApiRoutes.Base, Name = "[controller]_[action]")]
    public IActionResult Index()
    {
        return Redirect(ApiRoutes.SwaggerRessource);
    }
}