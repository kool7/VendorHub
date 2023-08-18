using FluentValidation;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.Application.Contracts.Validations.Vendors
{
    public class CreateVendorDtoValidator : AbstractValidator<CreateVendorDto>
    {
        public CreateVendorDtoValidator()
        {
            RuleFor(vendor =>  vendor.Name)
                .NotEmpty()
                .NotNull()
                .WithMessage(VendorHubResponse.VendorNameRequired)
                .MaximumLength(50)
                .WithMessage(VendorHubResponse.VendorNameLimit)
                .Matches("^[a-zA-Z0-9&\\s]*$").WithMessage(VendorHubResponse.VendorNameNotValid);

            RuleFor(vendor => vendor.Description)
                .NotEmpty()
                .NotNull()
                .WithMessage(VendorHubResponse.VendorDescriptionRequired);
        }
    }
}
