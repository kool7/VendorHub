using FluentValidation;
using Mapster;
using Microsoft.Extensions.DependencyInjection;
using VendorHub.Application.Contracts.Validations.Vendors;
using VendorHub.Application.Contracts.Vendors;
using VendorHub.Application.Vendors;

namespace VendorHub.Application;

public static class DependencyInjection
{
    public static IServiceCollection AddApplication(this IServiceCollection services)
    {
        services.AddSingleton(TypeAdapterConfig.GlobalSettings);
        services.AddValidatorsFromAssemblyContaining<CreateVendorDtoValidator>();
        services.AddScoped<IVendorService, VendorService>();
        return services;
    }
}
