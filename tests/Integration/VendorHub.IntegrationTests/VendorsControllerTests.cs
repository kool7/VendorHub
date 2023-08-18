using Microsoft.Extensions.Configuration;
using System.Net;
using System.Net.Http.Json;
using VendorHub.Api.Common;
using VendorHub.Application.Contracts.Validations;
using VendorHub.Application.Contracts.Vendors;
using VendorHub.Application.Vendors;
using VendorHub.Infrastructure.Repositories;
using VendorHub.Infrastructure.Utility;
using VendorHub.IntegrationTests.Utility;
using VendorHub.Utils;

namespace VendorHub.IntegrationTests;

public class VendorsControllerTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _appFactory;

    public VendorsControllerTests()
    {
        _appFactory = new WebApplicationFactory<Program>()
            .WithWebHostBuilder(builder =>
            {
                builder.ConfigureAppConfiguration((context, config) =>
                {
                    config.AddJsonFile("appsettings.Testing.json");
                });

                builder.ConfigureServices(services =>
                {
                    var serviceProvider = services.BuildServiceProvider();
                    var configuration = serviceProvider.GetRequiredService<IConfiguration>();
                    var mongoDbSettings = configuration.GetSection("MongoDB").Get<MongoDbSettings>();
                    services.AddSingleton<VendorDbContext>();
                    services.AddScoped<IVendorService, VendorService>();
                    services.AddScoped<IVendorRepository, VendorRepository>();
                });
            });

        _httpClient = _appFactory.CreateClient();
    }

    [Fact]
    public async Task CreateVendor_Success_EndpointReturnsOkResult()
    {
        // Arrange
        MongoDbConfig.InitializeDbForTests(_appFactory);
        var createVendorDto = VendorHubFixtures.GenerateCreateVendorDto();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, createVendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var readVendorDto = await response.Content.ReadFromJsonAsync<ReadVendorDto>();
        readVendorDto.Should().BeEquivalentTo(createVendorDto);
    }

    [Fact]
    public async Task CreateVendor_NameRequired_EndpointReturnsBadRequest()
    {
        // Arrange
        var vendorDto = VendorHubFixtures.GenerateCreateVendorDtoWithoutName();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, vendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var vendor = await response.Content.ReadAsStringAsync();
        vendor.Should().Contain(VendorHubResponse.VendorNameRequired);
    }

    [Fact]
    public async Task CreateVendor_InValidVendorName_EndpointReturnsBadRequest()
    {
        // Arrange
        var vendorDto = VendorHubFixtures.GenerateInValidCreateVendorDto();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, vendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var vendor = await response.Content.ReadAsStringAsync();
        vendor.Should().Contain(VendorHubResponse.VendorNameNotValid);
    }

    [Fact]
    public async Task CreateVendor_VendorNameLengthMoreThanMaxCharacterLimit_EndpointReturnsBadRequest()
    {
        // Arrange
        var vendorDto = VendorHubFixtures.GenerateDtoWithVendorNameMoreThanMaxCharLimit();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, vendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var vendor = await response.Content.ReadAsStringAsync();
        vendor.Should().Contain(VendorHubResponse.VendorNameLimit);
    }

    [Fact]
    public async Task CreateVendor_DescriptionRequired_EndpointReturnsBadRequest()
    {
        // Arrange
        var vendorDto = VendorHubFixtures.GenerateCreateVendorDtoWithoutDescription();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, vendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var vendor = await response.Content.ReadAsStringAsync();
        vendor.Should().Contain(VendorHubResponse.VendorDescriptionRequired);
    }
}