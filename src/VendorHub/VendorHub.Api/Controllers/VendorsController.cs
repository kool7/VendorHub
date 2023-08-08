using Microsoft.AspNetCore.Mvc;
using VendorHub.Api.Common;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Vendors)]
public class VendorsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateVendor(CreateVendorDto createVendorDto)
    {
        throw new NotImplementedException();
    }
}
