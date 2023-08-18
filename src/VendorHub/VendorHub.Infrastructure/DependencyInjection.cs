using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using VendorHub.Domain.Vendors;
using VendorHub.Infrastructure.Persistence;
using VendorHub.Infrastructure.Repositories;
using VendorHub.Infrastructure.Utility;

namespace VendorHub.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(
        this IServiceCollection services,
        ConfigurationManager configurationManager)
    {
        services.Configure<MongoDbSettings>(configurationManager.GetSection("MongoDB"));
        services.AddSingleton<VendorDbContext>();
        services.AddScoped<IVendorRepository, VendorRepository>();
        return services;
    }
}
