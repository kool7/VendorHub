using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class VendorsController : ControllerBase
{
    [HttpPost]
    public IActionResult CreateVendor(CreateVendorDto createVendorDto)
    {
        throw new NotImplementedException();
    }
}
