using VendorHub.Domain.Vendors;
using VendorHub.Infrastructure.Persistence;

namespace VendorHub.Infrastructure.Repositories
{
    public class VendorRepository : IVendorRepository
    {
        private readonly VendorDbContext _vendorDbContext;

        public VendorRepository(VendorDbContext vendorDbContext)
        {
            _vendorDbContext = vendorDbContext;
        }

        public async Task<Vendor> CreateAsync(Vendor vendor)
        {
            await _vendorDbContext.Vendors.InsertOneAsync(vendor);
            return vendor;
        }
    }
}
