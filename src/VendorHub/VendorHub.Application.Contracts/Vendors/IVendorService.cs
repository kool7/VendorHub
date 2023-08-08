namespace VendorHub.Application.Contracts.Vendors
{
    public interface IVendorService
    {
        Task<ReadVendorDto> CreateVendorAsync(CreateVendorDto createVendorDto);
    }
}
