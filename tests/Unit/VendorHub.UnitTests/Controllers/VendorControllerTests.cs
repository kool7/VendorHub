using Microsoft.AspNetCore.Mvc;
using VendorHub.Api.Controllers;
using VendorHub.Application.Contracts.Vendors;

namespace VendorHub.UnitTests.Controllers;

public class VendorControllerTests
{
    private readonly Fixture _fixture;
    private readonly Mock<IVendorService> _mockVendorService;
    private readonly VendorsController _sutVendorsController;

    public VendorControllerTests()
    {
        _fixture = new Fixture();
        _mockVendorService = new Mock<IVendorService>();
        _sutVendorsController = new VendorsController(_mockVendorService.Object);
    }

    [Fact]
    public void CreateVendor_ValidInput_ReturnsCreatedResponse()
    {
        // Arrange
        var createVendorDto = _fixture.Create<CreateVendorDto>();
        var readVendorDto = _fixture.Create<ReadVendorDto>();
        _mockVendorService
            .Setup(x => x.CreateVendorAsync(createVendorDto)).ReturnsAsync(readVendorDto);

        // Act
        var result = _sutVendorsController.CreateVendor(createVendorDto);

        // Assert
        result.Should().BeOfType<CreatedAtRouteResult>().Which.RouteName.Should().Be("GetVendor");
        result.Should().BeOfType<CreatedAtRouteResult>().Which.Value.Should().Be(readVendorDto);
    }

    [Fact]
    public void CreateVendor_InValidInput_ReturnsBadRequestResponse()
    {
        // Arrange
        var inValidDto = new CreateVendorDto
        {
            Name = string.Empty,
            Description = string.Empty
        };

        // Act
        var result = _sutVendorsController.CreateVendor(inValidDto);

        // Assert
        result.Should().BeOfType<BadRequestResult>();
    }
}
