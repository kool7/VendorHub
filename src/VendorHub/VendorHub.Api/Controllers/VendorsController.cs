using Microsoft.AspNetCore.Mvc;
using VendorHub.Api.Common;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Api.Controllers;

[ApiController]
[Route(ApiRoutes.Vendors)]
public class VendorsController : ControllerBase
{
    private readonly IVendorService _vendorService;

    public VendorsController(IVendorService vendorService)
    {
        _vendorService = vendorService;
    }

    [HttpPost]
    public async Task<IActionResult> CreateVendorAsync([FromBody] CreateVendorDto createVendorDto)
    {
        var result = await _vendorService.CreateAsync(createVendorDto);

        if (!result.IsSuccess)
        {
            return BadRequest(result.HasErrors);
        }

        return CreatedAtRoute("GetVendor", new { id = result.Data.Id }, result.Data);
    }
}
