using MongoDB.Bson;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Utils;

public static class Helper
{
    public static ReadVendorDto MapToDto(CreateVendorDto createVendorDto)
    {
        return new ReadVendorDto
        (
            Id: ObjectId.GenerateNewId().ToString(),
            Name: createVendorDto.Name,
            Description: createVendorDto.Description
        );
    }
}
