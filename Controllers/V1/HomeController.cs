using AutoMapper;
using hello_asp_identity.Contracts.V1;
using hello_asp_identity.Contracts.V1.Requests;
using hello_asp_identity.Contracts.V1.Responses;
using hello_asp_identity.Domain;
using hello_asp_identity.Extensions;
using hello_asp_identity.Helpers;
using hello_asp_identity.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Localization;

namespace hello_asp_identity.Controllers.V1;

public class HomeController : AppControllerBase
{
    private readonly IStringLocalizer<typeof(HomeController)> _localizer;
    public HomeController(IStringLocalizer<HomeController> localizer)
    {
        _localizer = localizer;
    }

    // start eventuell an anderer route
    // redirect eventuell zu anderer route

    [HttpGet(ApiRoutes.Base, Name = "[controller]_[action]")]
    public async Task<IActionResult> Index()
    {
        return Redirect(ApiRoutes.SwaggerRessource);
    }
}