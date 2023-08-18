using Mapster;
using VendorHub.Application.Contracts.Common;
using VendorHub.Application.Contracts.Validations.Vendors;
using VendorHub.Application.Contracts.Vendors;
using VendorHub.Domain.Vendors;

namespace VendorHub.Application.Vendors;

public class VendorService : IVendorService
{
    private readonly IVendorRepository _vendorRepository;
    private readonly CreateVendorDtoValidator _createVendorDtoValidator;

    public VendorService(
        IVendorRepository vendorRepository,
        CreateVendorDtoValidator createVendorDtoValidator)
    {
        _vendorRepository = vendorRepository;
        _createVendorDtoValidator = createVendorDtoValidator;
    }

    public async Task<ResultModel<ReadVendorDto>> CreateAsync(CreateVendorDto createVendorDto)
    {
        // Validating Dto
        var validationResult = await _createVendorDtoValidator.ValidateAsync(createVendorDto);
        if (!validationResult.IsValid)
        {
            return ResultModel<ReadVendorDto>.Error(validationResult.Errors.Select(e => e.ErrorMessage).ToList());
        }

        // Check for duplicate entry will be implemented when GetAsync method will be introduced

        // Mapping to domain model
        var vendor = createVendorDto.Adapt<Vendor>();

        // Adding vendor to database
        var addedVendor = await _vendorRepository.CreateAsync(vendor);

        // Mapping domain to Dto
        var readVendorDto = addedVendor.Adapt<ReadVendorDto>();

        // Return result
        return ResultModel<ReadVendorDto>.Success(readVendorDto);
    }
}
