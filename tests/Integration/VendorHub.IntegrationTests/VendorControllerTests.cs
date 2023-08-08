using System.Net;
using System.Net.Http.Json;
using VendorHub.Api.Common;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.IntegrationTests;

public class VendorControllerTests
    : IClassFixture<WebApplicationFactory<Program>>
{
    private readonly HttpClient _httpClient;
    private readonly WebApplicationFactory<Program> _appFactory;
    private readonly Fixture _fixture;

    public VendorControllerTests()
    {
        _appFactory = new WebApplicationFactory<Program>();
        _httpClient = _appFactory.CreateClient();
        _fixture = new Fixture();
    }

    [Fact]
    public async Task CreateVendor_Success_EndpointReturnsOkResult()
    {
        // Arrange
        var createVendorDto = _fixture.Create<CreateVendorDto>();

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, createVendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.Created);
        var readVendorDto = await response.Content.ReadFromJsonAsync<ReadVendorDto>();
        readVendorDto.Should().BeEquivalentTo(createVendorDto);
    }

    [Fact]
    public async Task CreateVendor_InvalidInput_EndpointReturnsBadRequest()
    {
        // Arrange
        var vendorDto = new CreateVendorDto
        {
            Name = string.Empty,
            Description = string.Empty
        };

        // Act
        var response = await _httpClient.PostAsJsonAsync(ApiRoutes.Vendors, vendorDto);

        // Assert
        response.StatusCode.Should().Be(HttpStatusCode.BadRequest);
        var vendor = await response.Content.ReadAsStringAsync();
        vendor.Should().Contain(VendorHubResponse.VendorNameRequired);
        vendor.Should().Contain(VendorHubResponse.VendorDescriptionRequired);
    }
}