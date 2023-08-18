using Mapster;
using VendorHub.Application.Contracts.Vendors;
using VendorHub.Domain.Vendors;

namespace VendorHub.Application.Mappings
{
    public class VendorHubMappings : IRegister
    {
        public void Register(TypeAdapterConfig config)
        {
            config.NewConfig<CreateVendorDto, Vendor>();
            config.NewConfig<Vendor, ReadVendorDto>().MapToConstructor(true);
        }
    }
}
