namespace VendorHub.Domain.Vendors
{
    public interface IVendorRepository
    {
        Task<Vendor> CreateAsync(Vendor vendor);
    }
}
