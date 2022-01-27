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

namespace hello_asp_identity.Controllers.V1;

public class HomeController : AppControllerBase
{
    public HomeController() { }

    // start eventuell an anderer route
    // redirect eventuell zu anderer route

    [HttpGet(ApiRoutes.Base, Name = "[controller]_[action]")]
    public async Task<IActionResult> Index()
    {
        return Redirect(ApiRoutes.SwaggerRessource);
    }
}