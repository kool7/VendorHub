using VendorHub.Application.Contracts.Vendors;
using VendorHub.Domain.Vendors;

namespace VendorHub.Application.Vendors;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;

    public VendorService(IVendorRepository vendorRepository)
    {
        _vendorRepository = vendorRepository;
    }

    public Task<ReadVendorDto> CreateVendorAsync(CreateVendorDto createVendorDto)
    {
        throw new NotImplementedException();
    }
}
