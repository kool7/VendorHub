namespace VendorHub.Application.Contracts.Vendors;

public record CreateVendorDto
{
    public string Name { get; init; }
    public string Description { get; init; }
}
