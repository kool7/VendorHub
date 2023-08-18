using VendorHub.Api;
using VendorHub.Application;
using VendorHub.Infrastructure;

var builder = WebApplication.CreateBuilder(args);
{
    builder.Services
        .AddPresentation()
        .AddApplication()
        .AddInfrastructure(builder.Configuration);
}


var app = builder.Build();
{
    app.MapControllers();
    app.Run();
}

public partial class Program { }
