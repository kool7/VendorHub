namespace VendorHub.Domain.Vendors
{
    public interface IVendorRepository
    {
        Task<Vendor> AddAsync(Vendor vendor);
    }
}
