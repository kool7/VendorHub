using VendorHub.Application.Contracts.Common;

namespace VendorHub.Application.Contracts.Vendors
{
    public interface IVendorService
    {
        Task<ResultModel<ReadVendorDto>> CreateAsync(CreateVendorDto createVendorDto);
    }
}
